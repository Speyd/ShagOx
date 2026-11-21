using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Orders.Core.Status;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core;
using ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
using ShagOxServer.Application.Interfaces.Repositories.Payments.Core;
using ShagOxServer.Application.Interfaces.Services.Orders.Core.Status;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Resources.Order;
using ShagOxServer.Application.Resources.Payment;
using ShagOxServer.Application.Services.Advertisements.Core.Mapping.Cache;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
using ShagOxServer.Application.Services.Caches.Invalidations.Orders;
using ShagOxServer.Application.Services.Orders.Core.Mapping;
using ShagOxServer.Domain.Entities.Deliveries.Enum;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Entities.Orders.Enum;
using ShagOxServer.Domain.Entities.Payments.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Enum;

namespace ShagOxServer.Application.Services.Orders.Core.Status;

/// <summary>
/// Життєвий цикл замовлення на сайті.
///
///   Pending ──confirm──► Confirmed ──ship──► Shipped / ReadyForShipment ──complete──► Completed
///      │                     │
///      └──cancel / reject────┴──► Cancelled / Rejected (товари повертаються на склад,
///                                  оплачене — кошти повертаються покупцю)
///
/// Кожен перехід — атомарний умовний UPDATE (WHERE OrderStatus IN ...), тому
/// одночасні дії (покупець скасовує, поки продавець підтверджує) не можуть
/// пройти обидві: друга отримає "Статус замовлення щойно змінився".
/// </summary>
public class OrderStatusService
    : IOrderStatusService
{
    /// <summary>Поки замовлення не передано в доставку, покупець може його скасувати.</summary>
    private static readonly OrderStatus[] BuyerCancellable =
        [OrderStatus.Pending, OrderStatus.Confirmed];

    private static readonly OrderStatus[] SellerRejectable =
        [OrderStatus.Pending, OrderStatus.Confirmed];

    private static readonly OrderStatus[] Completable =
    [
        OrderStatus.ReadyForShipment,
        OrderStatus.Shipped,
        OrderStatus.InTransit,
        OrderStatus.Delivered
    ];

    private const int MaxTrackingNumberLength = 50;
    private const string NovaPoshtaCode = "NOVA_POSHTA";


    private readonly IOrderCommandRepository _orderCommand;
    private readonly IPaymentCommandRepository _paymentCommand;
    private readonly IPaymentGateway _paymentGateway;

    private readonly IAdvertisementCommandRepository _advertCommand;
    private readonly IAdvertisementVariantCommandRepository _variantCommand;

    private readonly OrderInvalidationService _orderInvalidation;
    private readonly AdvertisementInvalidationService _advertInvalidation;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrderStatusService> _logger;


    public OrderStatusService(
        IOrderCommandRepository orderCommand,
        IPaymentCommandRepository paymentCommand,
        IPaymentGateway paymentGateway,
        IAdvertisementCommandRepository advertCommand,
        IAdvertisementVariantCommandRepository variantCommand,
        OrderInvalidationService orderInvalidation,
        AdvertisementInvalidationService advertInvalidation,
        IUnitOfWork unitOfWork,
        ILogger<OrderStatusService> logger)
    {
        _orderCommand = orderCommand;
        _paymentCommand = paymentCommand;
        _paymentGateway = paymentGateway;
        _advertCommand = advertCommand;
        _variantCommand = variantCommand;
        _orderInvalidation = orderInvalidation;
        _advertInvalidation = advertInvalidation;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    // ───────────────────────── Покупець ─────────────────────────

    public async Task<Result<OrderStatusResponse>> CancelByBuyerAsync(
        long orderId,
        long buyerId,
        CancellationToken cancellationToken = default)
    {
        var order = await _orderCommand
            .GetForPaymentAsync(orderId, cancellationToken);

        if (order is null || order.OwnerId != buyerId)
            return Result<OrderStatusResponse>.NotFound(EntityNamesResources.Order);

        if (!BuyerCancellable.Contains(order.OrderStatus))
            return Fail(OrderResources.OrderCancelNotAllowed);

        return await CloseAsync(
            order,
            BuyerCancellable,
            OrderStatus.Cancelled,
            cancellationToken);
    }

    // ───────────────────────── Продавець ─────────────────────────

    public async Task<Result<OrderStatusResponse>> ConfirmBySellerAsync(
        long orderId,
        long sellerId,
        CancellationToken cancellationToken = default)
    {
        var (order, error) = await GetSellerOrderAsync(
            orderId, sellerId, cancellationToken);

        if (error is not null)
            return error;

        if (order!.OrderStatus != OrderStatus.Pending)
            return Fail(OrderResources.OrderConfirmNotAllowed);

        if (order.PaymentMethod == PaymentMethod.Online &&
            order.PaymentStatus != OrderPaymentStatus.Paid)
        {
            return Fail(OrderResources.OrderConfirmUnpaid);
        }

        var changed = await _orderCommand
            .TryConfirmAsync(order.Id, cancellationToken);

        return await FinishAsync(order, changed, OrderStatus.Confirmed, stockChanged: false);
    }

    public async Task<Result<OrderStatusResponse>> RejectBySellerAsync(
        long orderId,
        long sellerId,
        CancellationToken cancellationToken = default)
    {
        var (order, error) = await GetSellerOrderAsync(
            orderId, sellerId, cancellationToken);

        if (error is not null)
            return error;

        if (!SellerRejectable.Contains(order!.OrderStatus))
            return Fail(OrderResources.OrderRejectNotAllowed);

        return await CloseAsync(
            order,
            SellerRejectable,
            OrderStatus.Rejected,
            cancellationToken);
    }

    public async Task<Result<OrderStatusResponse>> ShipBySellerAsync(
        long orderId,
        long sellerId,
        string? trackingNumber,
        CancellationToken cancellationToken = default)
    {
        var (order, error) = await GetSellerOrderAsync(
            orderId, sellerId, cancellationToken);

        if (error is not null)
            return error;

        if (order!.OrderStatus != OrderStatus.Confirmed)
            return Fail(OrderResources.OrderShipNotAllowed);

        // Самовивіз: посилки немає, замовлення просто готове до видачі.
        if (order.DeliveryType == DeliveryType.Pickup)
        {
            var ready = await _orderCommand.TryShipAsync(
                order.Id,
                OrderStatus.ReadyForShipment,
                trackingNumber: null,
                trackingUrl: null,
                cancellationToken);

            return await FinishAsync(order, ready, OrderStatus.ReadyForShipment, stockChanged: false);
        }

        var number = trackingNumber?.Trim();

        if (string.IsNullOrEmpty(number) || number.Length > MaxTrackingNumberLength)
            return Fail(OrderResources.OrderTrackingNumberRequired);

        var trackingUrl = order.DeliveryProvider?.Code == NovaPoshtaCode
            ? $"https://novaposhta.ua/tracking/?cargo_number={Uri.EscapeDataString(number)}"
            : null;

        var shipped = await _orderCommand.TryShipAsync(
            order.Id,
            OrderStatus.Shipped,
            number,
            trackingUrl,
            cancellationToken);

        return await FinishAsync(order, shipped, OrderStatus.Shipped, stockChanged: false);
    }

    // ───────────────────────── Обидві сторони ─────────────────────────

    public async Task<Result<OrderStatusResponse>> CompleteAsync(
        long orderId,
        long userId,
        CancellationToken cancellationToken = default)
    {
        var order = await _orderCommand
            .GetForPaymentAsync(orderId, cancellationToken);

        if (order is null)
            return Result<OrderStatusResponse>.NotFound(EntityNamesResources.Order);

        var isBuyer = order.OwnerId == userId;

        if (!isBuyer)
        {
            var access = CheckSellerAccess(order, userId);
            if (access is not null)
                return access;
        }

        if (!Completable.Contains(order.OrderStatus))
            return Fail(OrderResources.OrderCompleteNotAllowed);

        var changed = await _orderCommand.TryCompleteAsync(
            order.Id,
            Completable,
            cancellationToken);

        return await FinishAsync(order, changed, OrderStatus.Completed, stockChanged: false);
    }

    // ───────────────────────── Спільна логіка ─────────────────────────

    /// <summary>
    /// Скасування / відхилення: товари повертаються на склад; оплачене
    /// замовлення — кошти повертаються покупцю; неоплачений рахунок анулюється.
    /// Якщо банк не прийняв повернення, нічого не змінюється.
    /// </summary>
    private async Task<Result<OrderStatusResponse>> CloseAsync(
        Order order,
        IReadOnlyCollection<OrderStatus> allowedFrom,
        OrderStatus newStatus,
        CancellationToken cancellationToken)
    {
        var expectedPaymentStatus = order.PaymentStatus;
        var isPaidOnline =
            order.PaymentMethod == PaymentMethod.Online &&
            order.PaymentStatus == OrderPaymentStatus.Paid;

        var paidPayment = isPaidOnline
            ? await _paymentCommand.GetSucceededByOrderIdAsync(order.Id, cancellationToken)
            : null;

        if (isPaidOnline && paidPayment is null)
        {
            _logger.LogError(
                "Paid order has no succeeded payment to refund. OrderId: {OrderId}",
                order.Id);

            return Fail(PaymentResources.PaymentRefundFailed, ResultErrorType.InternalServer);
        }

        var openPayments = order.PaymentMethod == PaymentMethod.Online && !isPaidOnline
            ? await _paymentCommand.GetOpenByOrderIdAsync(order.Id, cancellationToken)
            : [];

        var changedAt = DateTime.UtcNow;

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            // Спершу закриваємо відкриті рахунки — у тому ж порядку блокувань,
            // що й воркер прострочення (платіж → замовлення), щоб не було дедлоку.
            foreach (var payment in openPayments)
            {
                await _paymentCommand.TryMarkAsFailedAsync(
                    payment.InvoiceId,
                    cancellationToken);
            }

            var closed = await _orderCommand.TryCloseAsync(
                order.Id,
                allowedFrom,
                newStatus,
                expectedPaymentStatus,
                cancellationToken);

            if (!closed)
            {
                await _unitOfWork.RollbackAsync();

                return Fail(OrderResources.OrderStatusChanged, ResultErrorType.Conflict);
            }

            await ReturnStockAsync(order, cancellationToken);

            if (paidPayment is not null)
            {
                // Рядок замовлення вже заблоковано нашим UPDATE, тож статус
                // не зміниться, поки банк обробляє запит на повернення.
                var refund = await _paymentGateway.RefundAsync(
                    paidPayment.InvoiceId,
                    cancellationToken);

                if (!refund.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();

                    return Fail(refund.Error!, ResultErrorType.BadRequest);
                }

                await _paymentCommand.TryUpdatePaymentStatusAsync(
                    paidPayment.InvoiceId,
                    PaymentStatus.Refunded,
                    cancellationToken);

                await _orderCommand.TryUpdatePaymentStatusAsync(
                    order.Id,
                    OrderPaymentStatus.Refunded,
                    cancellationToken);
            }

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to close order. OrderId: {OrderId}, NewStatus: {Status}",
                order.Id,
                newStatus);

            return Fail(OrderResources.OrderStatusUpdateFailed, ResultErrorType.InternalServer);
        }

        // Анулюємо посилання в банку, щоб за ним не можна було заплатити.
        // Якщо не вийде — нічого страшного: рахунок і так закриється сам,
        // а запізнілу оплату обробник вебхуків поверне автоматично.
        foreach (var payment in openPayments)
        {
            await _paymentGateway.CancelInvoiceAsync(payment.InvoiceId, cancellationToken);
        }

        _logger.LogInformation(
            "Order closed. OrderId: {OrderId}, {From} -> {To}, Refunded: {Refunded}",
            order.Id,
            order.OrderStatus,
            newStatus,
            paidPayment is not null);

        await InvalidateCacheAsync(order, stockChanged: true);

        return Result<OrderStatusResponse>.Success(
            new OrderStatusResponse(order.Id, newStatus, changedAt));
    }

    private async Task<Result<OrderStatusResponse>> FinishAsync(
        Order order,
        bool changed,
        OrderStatus newStatus,
        bool stockChanged)
    {
        if (!changed)
            return Fail(OrderResources.OrderStatusChanged, ResultErrorType.Conflict);

        _logger.LogInformation(
            "Order status changed. OrderId: {OrderId}, {From} -> {To}",
            order.Id,
            order.OrderStatus,
            newStatus);

        await InvalidateCacheAsync(order, stockChanged);

        return Result<OrderStatusResponse>.Success(
            new OrderStatusResponse(order.Id, newStatus, DateTime.UtcNow));
    }

    private async Task<(Order? Order, Result<OrderStatusResponse>? Error)> GetSellerOrderAsync(
        long orderId,
        long sellerId,
        CancellationToken cancellationToken)
    {
        var order = await _orderCommand
            .GetForPaymentAsync(orderId, cancellationToken);

        if (order is null)
            return (null, Result<OrderStatusResponse>.NotFound(EntityNamesResources.Order));

        return (order, CheckSellerAccess(order, sellerId));
    }

    /// <summary>
    /// Продавець керує замовленням, лише якщо всі товари в ньому — його.
    /// Нові замовлення завжди від одного продавця; перевірка захищає старі
    /// "змішані" замовлення, де один продавець вирішував би долю чужих товарів.
    /// </summary>
    private static Result<OrderStatusResponse>? CheckSellerAccess(
        Order order,
        long sellerId)
    {
        var items = order.OrderItems;

        if (!items.Any(i => i.Advertisement.OwnerId == sellerId))
            return Fail(OrderResources.OrderNotSellerOrder, ResultErrorType.Forbidden);

        if (items.Any(i => i.Advertisement.OwnerId != sellerId))
            return Fail(OrderResources.OrderHasOtherSellersItems, ResultErrorType.Forbidden);

        return null;
    }

    private async Task ReturnStockAsync(
        Order order,
        CancellationToken cancellationToken)
    {
        // Так само, як при створенні: залишок рахується по варіанту,
        // якщо він є, інакше — по самому оголошенню.
        foreach (var item in order.OrderItems)
        {
            if (item.AdvertisementVariantId is long variantId)
            {
                await _variantCommand.IncreaseStockAsync(
                    variantId, item.Quantity, cancellationToken);
            }
            else
            {
                await _advertCommand.IncreaseStockAsync(
                    item.AdvertisementId, item.Quantity, cancellationToken);
            }
        }
    }

    private async Task InvalidateCacheAsync(
        Order order,
        bool stockChanged)
    {
        try
        {
            await _orderInvalidation.InvalidateUpdateAsync(
                OrderCacheMapper.ToInfo(order));

            if (!stockChanged)
                return;

            var adverts = order.OrderItems
                .Select(i => i.Advertisement)
                .DistinctBy(a => a.Id);

            foreach (var advert in adverts)
            {
                await _advertInvalidation.InvalidateUpdateAsync(
                    AdvertisementCacheMapper.ToInfo(advert));
            }
        }
        catch (Exception ex)
        {
            // Статус уже збережено — застарілий кеш не має перетворюватися на помилку.
            _logger.LogWarning(
                ex,
                "Failed to invalidate cache after order status change. OrderId: {OrderId}",
                order.Id);
        }
    }

    private static Result<OrderStatusResponse> Fail(
        string error,
        ResultErrorType errorType = ResultErrorType.BadRequest)
    {
        return Result<OrderStatusResponse>.Fail(error, errorType);
    }
}
