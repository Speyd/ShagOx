using CloudinaryDotNet;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Orders.Core.Create;
using ShagOxServer.Application.DTOs.Payment;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
using ShagOxServer.Application.Interfaces.Services.Deliveries.Calculators;
using ShagOxServer.Application.Interfaces.Services.Orders;
using ShagOxServer.Application.Interfaces.Services.Orders.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Orders.OrderItems.Create;
using ShagOxServer.Application.Interfaces.Services.Payments;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Resources.Order;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Orders;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Validator;
using ShagOxServer.Application.Services.Orders.Core.Mapping;
using ShagOxServer.Application.Services.Specification.Currencies.Validator;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Entities.Payments.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Orders.Core.Create;

public partial class OrderCreateService 
    : IOrderCreateService
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IOrderCommandRepository _orderCommandRepository;

    private readonly UserValidator _userValidator;
    private readonly DeliveryProviderValidator _deliveryValidator;

    private readonly IDeliveryPriceCalculator _deliveryCalculator;
    private readonly IOrderItemCreateService _orderItemCreateService;
    private readonly IOrderNumberGenerator _orderNumberGenerator;
    private readonly IPaymentProvider _paymentProvider;
    private readonly IPaymentWebhookProcessor _webhookProcessor;

    private readonly ICurrencyProvider _currencyProvider;

    private readonly OrderInvalidationService _orderInvalidation;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrderCreateService> _logger;

    public OrderCreateService(
        IRepository<Order> orderRepository,
        IOrderCommandRepository orderCommandRepository,
        UserValidator userValidator,
        DeliveryProviderValidator deliveryValidator,
        IDeliveryPriceCalculator deliveryCalculator,
        IOrderItemCreateService orderItemCreateService,
        IPaymentProvider paymentProvider,
        IPaymentWebhookProcessor webhookProcessor,
        OrderInvalidationService orderInvalidation,
        IOrderNumberGenerator orderNumberGenerator,
        ICurrencyProvider currencyProvider,
        IUnitOfWork unitOfWork,
        ILogger<OrderCreateService> logger)
    {
        _orderRepository = orderRepository;
        _orderCommandRepository = orderCommandRepository;

        _userValidator = userValidator;
        _deliveryValidator = deliveryValidator;

        _deliveryCalculator = deliveryCalculator;
        _orderItemCreateService = orderItemCreateService;
        _orderNumberGenerator = orderNumberGenerator;

        _paymentProvider = paymentProvider;
        _webhookProcessor = webhookProcessor;

        _currencyProvider = currencyProvider;

        _orderInvalidation = orderInvalidation;

        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<OrderCreateResponse>> CreateAsync(
        OrderCreateRequest request,
        long ownerId,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await ValidateOrderAsync(
            ownerId,
            request,
            cancellationToken);

        if (!validationResult.IsSuccess)
        {
            return Result<OrderCreateResponse>
                .Fail(validationResult.Error);
        }

        await _unitOfWork.BeginTransactionAsync();

        Order order;

        try
        {
            order = await CreateOrderEntityAsync(
                ownerId,
                validationResult.Value.Item2,
                validationResult.Value.Item1,
                request);

            var itemsResult = await CreateOrderItemsAsync(
                order,
                request);

            if (!itemsResult.IsSuccess)
            {
                await _unitOfWork.RollbackAsync();

                return Result<OrderCreateResponse>
                    .Fail(itemsResult.Error);
            }

            // Одне замовлення — один продавець: саме він підтверджує,
            // відправляє й отримує оплату за свої товари.
            var sellerIds = await _orderCommandRepository
                .GetSellerIdsAsync(order.Id, cancellationToken);

            if (sellerIds.Count != 1)
            {
                await _unitOfWork.RollbackAsync();

                return Result<OrderCreateResponse>
                    .Fail(OrderResources.OrderMultipleSellers);
            }

            var amountReult = await CalculateOrderAmountsAsync(
                order,
                cancellationToken);

            if(!amountReult.IsSuccess)
            {
                await _unitOfWork.RollbackAsync();

                return Result<OrderCreateResponse>
                    .Fail(amountReult.Error);
            }    


            await _unitOfWork.CommitAsync();

            await _orderInvalidation.InvalidateCreateAsync(
                OrderCacheMapper.ToInfo(order));
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create order. UserId: {UserId}",
                ownerId);

            return Result<OrderCreateResponse>
                .Fail(EntityErrorResources.OrderCreateFailed);
        }

        if (order.PaymentMethod != PaymentMethod.Online)
        {
            return Result<OrderCreateResponse>.Success(
                new OrderCreateResponse(
                    order.Id,
                    null,
                    DateTime.UtcNow));
        }

        try
        {
            var paymentResult = await MakePayment(
                order,
                cancellationToken);

            if (!paymentResult.IsSuccess)
            {
                // Замовлення вже створено й товари зарезервовано,
                // тому не повертаємо помилку (інакше користувач оформить
                // його вдруге). Без посилання фронтенд відкриє сторінку
                // замовлення, де можна натиснути "Оплатити".
                _logger.LogWarning(
                    "Payment link was not created. OrderId: {OrderId}, Error: {Error}",
                    order.Id,
                    paymentResult.Error);

                return Result<OrderCreateResponse>.Success(
                    new OrderCreateResponse(
                        order.Id,
                        null,
                        order.CreatedAt));
            }

            await _orderInvalidation.InvalidateCreateAsync(
                OrderCacheMapper.ToInfo(order));

            return Result<OrderCreateResponse>.Success(
                new OrderCreateResponse(
                    order.Id,
                    paymentResult.Value,
                    DateTime.UtcNow));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Payment failed. OrderId: {OrderId}",
                order.Id);

            return Result<OrderCreateResponse>.Success(
                new OrderCreateResponse(
                    order.Id,
                    null,
                    order.CreatedAt));
        }
    }

    private async Task<Result<string>> MakePayment(
        Order order,
        CancellationToken cancellationToken)
    {
        var request = new PaymentRequest
        (
            order.TotalPrice,
            order.Id,
            order.Currency.Ccy,
            order.Currency.DecimalPlaces
        );
 
        var result = await _paymentProvider
            .PaymentRequest(request, cancellationToken);

        if (!result.IsSuccess || result.Value is null)
            return Result<string>.Fail(result.Error);

        order.PaymentStatus = OrderPaymentStatus.Pending;

        _orderRepository.Update(order);

        await _unitOfWork.SaveChangesAsync();

        await _webhookProcessor.ProcessPendingAsync(
            result.Value.InvoiceId,
            cancellationToken);

        return Result<string>.Success(result.Value.PaymentUrl);
    }

    private async Task<Result<bool>> CalculateOrderAmountsAsync(
        Order order,
        CancellationToken cancellationToken)
    {
        var price = await _deliveryCalculator
            .CalculateAsync(order, cancellationToken);

        if (!price.IsSuccess)
            return Result<bool>.Fail(price.Error);

        order.DeliveryPrice = price.Value;

        order.TotalPrice = order.Subtotal + order.DeliveryPrice;

        return Result<bool>.Success(true);
    }
}
