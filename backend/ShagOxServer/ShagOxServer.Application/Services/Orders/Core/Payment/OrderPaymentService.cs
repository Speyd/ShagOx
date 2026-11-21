using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Orders.Core.Payment;
using ShagOxServer.Application.DTOs.Payment;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core;
using ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
using ShagOxServer.Application.Interfaces.Repositories.Payments.Core;
using ShagOxServer.Application.Interfaces.Services.Orders.Core.Payment;
using ShagOxServer.Application.Interfaces.Services.Payments;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Resources.Order;
using ShagOxServer.Application.Resources.Payment;
using ShagOxServer.Application.Services.Caches.Invalidations.Orders;
using ShagOxServer.Application.Services.Orders.Core.Mapping;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements.Enum;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Entities.Orders.Enum;
using ShagOxServer.Domain.Entities.Payments.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Orders.Core.Payment;

public class OrderPaymentService
    : IOrderPaymentService
{
    private readonly IOrderCommandRepository _orderCommandRepository;
    private readonly IPaymentCommandRepository _paymentCommandRepository;

    private readonly IPaymentProcessingService _paymentProcessing;
    private readonly IPaymentProvider _paymentProvider;

    private readonly IAdvertisementCommandRepository _advertCommand;
    private readonly IAdvertisementVariantCommandRepository _variantCommand;

    private readonly OrderInvalidationService _orderInvalidation;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrderPaymentService> _logger;


    public OrderPaymentService(
        IOrderCommandRepository orderCommandRepository,
        IPaymentCommandRepository paymentCommandRepository,
        IPaymentProcessingService paymentProcessing,
        IPaymentProvider paymentProvider,
        IAdvertisementCommandRepository advertCommand,
        IAdvertisementVariantCommandRepository variantCommand,
        OrderInvalidationService orderInvalidation,
        IUnitOfWork unitOfWork,
        ILogger<OrderPaymentService> logger)
    {
        _orderCommandRepository = orderCommandRepository;
        _paymentCommandRepository = paymentCommandRepository;

        _paymentProcessing = paymentProcessing;
        _paymentProvider = paymentProvider;

        _advertCommand = advertCommand;
        _variantCommand = variantCommand;

        _orderInvalidation = orderInvalidation;

        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<OrderPaymentResponse>> GetOrCreatePaymentAsync(
        long orderId,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        var order = await _orderCommandRepository
            .GetForPaymentAsync(orderId, cancellationToken);

        if (order is null)
        {
            return Result<OrderPaymentResponse>.NotFound(
                EntityNamesResources.Order);
        }

        var stateError = ValidatePayable(order, now);

        if (stateError is not null)
            return Result<OrderPaymentResponse>.Fail(stateError);

        var needsReopen = order.OrderStatus == OrderStatus.Cancelled;

        if (!needsReopen)
        {
            // 1. Посилання ще діє — просто віддаємо його.
            var active = await _paymentCommandRepository
                .GetActiveByOrderIdAsync(orderId, now, cancellationToken);

            if (active?.PaymentUrl is not null)
            {
                return Result<OrderPaymentResponse>.Success(
                    new OrderPaymentResponse(
                        active.PaymentUrl,
                        active.ExpiresAt));
            }

            var open = await _paymentCommandRepository
                .GetOpenByOrderIdAsync(orderId, cancellationToken);

            // 2. Платіж ще обробляється банком — новий рахунок не створюємо,
            //    щоб уникнути подвійного списання.
            if (open.Any(p => !OrderPaymentPolicy.IsExpired(p, now)))
            {
                return Result<OrderPaymentResponse>.Fail(
                    PaymentResources.OrderPaymentInProgress);
            }

            // 3. Посилання прострочене, але воркер його ще не обробив.
            //    Закриваємо платіж тим самим шляхом, що й воркер
            //    (замовлення скасовується, товари повертаються на склад),
            //    а далі відновлюємо замовлення з новим рахунком.
            foreach (var payment in open)
            {
                var failed = await _paymentProcessing
                    .ProcessFailedPayment(payment, cancellationToken);

                if (!failed.IsSuccess)
                {
                    return Result<OrderPaymentResponse>
                        .Fail(failed.Error);
                }
            }

            // Якщо відкритих платежів не було взагалі (наприклад, провайдер
            // був недоступний під час оформлення), замовлення досі Pending,
            // а товари зарезервовані — достатньо створити рахунок.
            needsReopen = open.Count > 0;
        }

        return await CreatePaymentAsync(
            order,
            needsReopen,
            cancellationToken);
    }

    private static string? ValidatePayable(Order order, DateTime now)
    {
        if (order.PaymentMethod != PaymentMethod.Online)
            return PaymentResources.OrderPaymentNotRequired;

        if (order.PaymentStatus == OrderPaymentStatus.Paid)
            return PaymentResources.OrderAlreadyPaid;

        if (!OrderPaymentPolicy.CanPay(order, now))
            return PaymentResources.OrderPaymentUnavailable;

        return null;
    }

    private async Task<Result<OrderPaymentResponse>> CreatePaymentAsync(
        Order order,
        bool reopen,
        CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            if (reopen)
            {
                var reopened = await _orderCommandRepository
                    .TryReopenForPaymentAsync(order.Id, cancellationToken);

                if (!reopened)
                {
                    await _unitOfWork.RollbackAsync();

                    return Result<OrderPaymentResponse>.Fail(
                        PaymentResources.OrderPaymentUnavailable);
                }

                var stockResult = await ReserveStockAsync(
                    order,
                    cancellationToken);

                if (!stockResult.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();

                    return Result<OrderPaymentResponse>
                        .Fail(stockResult.Error);
                }
            }

            var paymentResult = await _paymentProvider.PaymentRequest(
                new PaymentRequest(
                    order.TotalPrice,
                    order.Id,
                    order.Currency.Ccy,
                    order.Currency.DecimalPlaces),
                cancellationToken);

            if (!paymentResult.IsSuccess || paymentResult.Value is null)
            {
                await _unitOfWork.RollbackAsync();

                return Result<OrderPaymentResponse>
                    .Fail(paymentResult.Error);
            }

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create payment. OrderId: {OrderId}",
                order.Id);

            return Result<OrderPaymentResponse>.Fail(
                PaymentResources.PaymentProviderInvalidResponse);
        }

        await _orderInvalidation.InvalidateUpdateAsync(
            OrderCacheMapper.ToInfo(order));

        var created = await _paymentCommandRepository
            .GetActiveByOrderIdAsync(
                order.Id,
                DateTime.UtcNow,
                cancellationToken);

        if (created?.PaymentUrl is null)
        {
            return Result<OrderPaymentResponse>.Fail(
                PaymentResources.PaymentProviderInvalidResponse);
        }

        _logger.LogInformation(
            "Payment link issued. OrderId: {OrderId}, Reopened: {Reopened}",
            order.Id,
            reopen);

        return Result<OrderPaymentResponse>.Success(
            new OrderPaymentResponse(
                created.PaymentUrl,
                created.ExpiresAt));
    }

    /// <summary>
    /// Повторно резервує товари скасованого замовлення.
    /// Списання атомарне (UPDATE ... WHERE Stock >= qty), тож якщо товару
    /// вже не вистачає, вся транзакція відкочується.
    /// </summary>
    private async Task<Result<bool>> ReserveStockAsync(
        Order order,
        CancellationToken cancellationToken)
    {
        foreach (var item in order.OrderItems)
        {
            var advert = item.Advertisement;

            var isAvailable =
                advert.EntityStatus == EntityStatus.Active &&
                advert.Status.Code == AdvertisementStatus.Active &&
                (item.AdvertisementVariant is null ||
                 item.AdvertisementVariant.EntityStatus == EntityStatus.Active);

            if (!isAvailable)
            {
                return Result<bool>.Fail(string.Format(
                    EntityErrorResources.ProoductNotAvailable,
                    advert.Title));
            }

            var reserved = item.AdvertisementVariantId.HasValue
                ? await _variantCommand.DecreaseStockAsync(
                    item.AdvertisementVariantId.Value,
                    item.Quantity,
                    cancellationToken)
                : await _advertCommand.DecreaseStockAsync(
                    item.AdvertisementId,
                    item.Quantity,
                    cancellationToken);

            if (!reserved)
            {
                return Result<bool>.Fail(string.Format(
                    OrderResources.InsufficientStock,
                    advert.Title));
            }
        }

        return Result<bool>.Success(true);
    }
}
