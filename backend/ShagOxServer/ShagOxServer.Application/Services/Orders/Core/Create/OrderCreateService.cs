using CloudinaryDotNet;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Builders;
using ShagOxServer.Application.Common.Options.Systems;
using ShagOxServer.Application.DTOs.Orders.Core.Create;
using ShagOxServer.Application.DTOs.Payment;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Providers.Payment;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Deliveries.Calculators;
using ShagOxServer.Application.Interfaces.Services.Orders;
using ShagOxServer.Application.Interfaces.Services.Orders.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Orders.OrderItems.Create;
using ShagOxServer.Application.Interfaces.Services.Payments;
using ShagOxServer.Application.Interfaces.Services.Verifications.Sending;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Orders;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Validator;
using ShagOxServer.Application.Services.Orders.Core.Mapping;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Orders.Core.Create;

public partial class OrderCreateService 
    : IOrderCreateService
{
    private readonly IRepository<Order> _orderRepository;

    private readonly UserValidator _userValidator;
    private readonly DeliveryProviderValidator _deliveryValidator;

    private readonly IDeliveryPriceCalculator _deliveryCalculator;
    private readonly IOrderItemCreateService _orderItemCreateService;
    private readonly IOrderNumberGenerator _orderNumberGenerator;
    private readonly IPaymentProvider _paymentProvider;
    private readonly IPaymentWebhookProcessor _webhookProcessor;

    private readonly ICurrencyProvider _currencyProvider;

    private readonly IAdvertisementCommandRepository _advertCommand;
    private readonly IAdvertisementVariantCommandRepository _variantProvider;


    private readonly OrderInvalidationService _orderInvalidation;

    private readonly IEmailService _emailService;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrderCreateService> _logger;



    public OrderCreateService(
        IRepository<Order> orderRepository,
        UserValidator userValidator,
        DeliveryProviderValidator deliveryValidator,
        IDeliveryPriceCalculator deliveryCalculator,
        IOrderItemCreateService orderItemCreateService,
        IPaymentProvider paymentProvider,
        IPaymentWebhookProcessor webhookProcessor,
        IAdvertisementCommandRepository advertCommand,
        IAdvertisementVariantCommandRepository variantProvider,
        OrderInvalidationService orderInvalidation,
        IOrderNumberGenerator orderNumberGenerator,
        ICurrencyProvider currencyProvider,
        IEmailService emailService,
        IUnitOfWork unitOfWork,
        ILogger<OrderCreateService> logger)
    {
        _orderRepository = orderRepository;

        _userValidator = userValidator;
        _deliveryValidator = deliveryValidator;

        _deliveryCalculator = deliveryCalculator;
        _orderItemCreateService = orderItemCreateService;
        _orderNumberGenerator = orderNumberGenerator;

        _paymentProvider = paymentProvider;
        _webhookProcessor = webhookProcessor;

        _currencyProvider = currencyProvider;

        _advertCommand = advertCommand;
        _variantProvider = variantProvider;

        _orderInvalidation = orderInvalidation;

        _emailService = emailService;

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

        var paymentResult = await InitiateOrderPaymentAsync(
            order, 
            cancellationToken);

        if(!paymentResult.IsSuccess)
        {
            await ResetStockAsync(order, cancellationToken);

            return Result<OrderCreateResponse>
                .Fail(paymentResult.Error);
        }

        return paymentResult;
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

    private async Task ResetStockAsync(
        Order order,
        CancellationToken cancellationToken)
    {
        foreach(var item in order.OrderItems)
        {
            if(item.AdvertisementVariantId.HasValue)
            {
                await _variantProvider.IncreaseStockAsync(
                    item.AdvertisementVariantId.Value,
                    item.Quantity,
                    cancellationToken);
            }
            else
            {
                await _advertCommand.IncreaseStockAsync(
                    item.AdvertisementId,
                    item.Quantity,
                    cancellationToken);
            }
        }
    }
}
