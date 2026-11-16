using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Orders.Core.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Deliveries.Calculators;
using ShagOxServer.Application.Interfaces.Services.Orders;
using ShagOxServer.Application.Interfaces.Services.Orders.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Orders.OrderItems.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Orders;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Validator;
using ShagOxServer.Application.Services.Orders.Core.Mapping;
using ShagOxServer.Application.Services.Specification.Currencies.Validator;
using ShagOxServer.Domain.Entities.Deliveries;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Orders.Core.Create;

public class OrderCreateService : IOrderCreateService
{
    private readonly IRepository<Order> _orderRepository;

    private readonly UserValidator _userValidator;
    private readonly DeliveryProviderValidator _deliveryValidator;
    private readonly CurrencyValidator _currencyValidator;

    private readonly IDeliveryPriceCalculator _deliveryCalculator;
    private readonly IOrderItemCreateService _orderItemCreateService;
    private readonly IOrderNumberGenerator _orderNumberGenerator;

    private readonly OrderInvalidationService _orderInvalidation;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrderCreateService> _logger;

    public OrderCreateService(
        IRepository<Order> orderRepository,
        UserValidator userValidator,
        DeliveryProviderValidator deliveryValidator,
        CurrencyValidator currencyValidator,
        IDeliveryPriceCalculator deliveryCalculator,
        IOrderItemCreateService orderItemCreateService,
        OrderInvalidationService orderInvalidation,
        IOrderNumberGenerator orderNumberGenerator,
        IUnitOfWork unitOfWork,
        ILogger<OrderCreateService> logger)
    {
        _orderRepository = orderRepository;

        _userValidator = userValidator;
        _deliveryValidator = deliveryValidator;
        _currencyValidator = currencyValidator;

        _deliveryCalculator = deliveryCalculator;
        _orderItemCreateService = orderItemCreateService;
        _orderNumberGenerator = orderNumberGenerator;

        _orderInvalidation = orderInvalidation;

        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
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
            return Result<CreateResponse>
                .Fail(validationResult.Error);
        }

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var order = await CreateOrderEntityAsync(
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

                return Result<CreateResponse>
                    .Fail(itemsResult.Error);
            }

            var amountReult = await CalculateOrderAmountsAsync(
                order,
                cancellationToken);

            if(!amountReult.IsSuccess)
            {
                return Result<CreateResponse>
                    .Fail(amountReult.Error);
            }    


            await _unitOfWork.CommitAsync();

            await _orderInvalidation.InvalidateCreateAsync(
                OrderCacheMapper.ToInfo(order));

            return Result<CreateResponse>.Success(
                new CreateResponse(
                    order.Id,
                    DateTime.UtcNow));
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create order. UserId: {UserId}",
                ownerId);

            return Result<CreateResponse>
                .Fail(EntityErrorResources.OrderCreateFailed);
        }
    }


    private async Task<Result<(Currency, DeliveryProvider?)>> ValidateOrderAsync(
        long ownerId,
        OrderCreateRequest request,
        CancellationToken cancellationToken)
    {
        var userResult = await _userValidator
            .ExistsByIdAsync(ownerId, cancellationToken);

        if (!userResult.IsSuccess)
        {
            return Result<(Currency, DeliveryProvider?)>.Fail(userResult.Error);
        }

        DeliveryProvider? deliveryProvider = null;
        if (request.DeliveryProviderId.HasValue)
        {
            var deliveryResult = await _deliveryValidator
                .GetByIdAsync(
                    request.DeliveryProviderId.Value,
                    cancellationToken);

            if (!deliveryResult.IsSuccess)
            {
                return Result<(Currency, DeliveryProvider?)>.Fail(deliveryResult.Error);
            }

            deliveryProvider = deliveryResult.Value;
        }

        var currencyResult = await _currencyValidator
            .GetByIdAsync(
                request.CurrencyId,
                cancellationToken);

        if (!currencyResult.IsSuccess ||
            currencyResult.Value is null)
        {
            return Result<(Currency, DeliveryProvider?)>
                .Fail(currencyResult.Error);
        }

        return Result<(Currency, DeliveryProvider?)>.Success(
            (currencyResult!.Value, deliveryProvider));
    }

    private async Task<Order> CreateOrderEntityAsync(
        long ownerId,
        DeliveryProvider? deliveryProvider,
        Currency currency,
        OrderCreateRequest request)
    {
        var orderNumber = _orderNumberGenerator.Generate();

        var order = OrderCreater.Create(ownerId, orderNumber, request);

        _orderRepository.Add(order);

        order.DeliveryProvider = deliveryProvider;
        order.Currency = currency;

        await _unitOfWork.SaveChangesAsync();

        return order;
    }

    private async Task<Result<bool>> CreateOrderItemsAsync(
        Order order,
        OrderCreateRequest request)
    {
        order.Subtotal = 0;
        order.TotalWeight = 0;

        foreach (var item in request.OrderItems)
        {
            var itemResult = await _orderItemCreateService
                .CreateInternalAsync(item with
                {
                    ParentId = order.Id
                });

            if (!itemResult.IsSuccess)
            {
                return Result<bool>.Fail(itemResult.Error);
            }

            order.Subtotal += itemResult.Value!.TotalPrice;
            order.TotalWeight += itemResult.Value.TotalWeight;
        }

        return Result<bool>.Success(true);
    }

    private async Task<Result<bool>> CalculateOrderAmountsAsync(
        Order order,
        CancellationToken cancellationToken)
    {
        var price = await _deliveryCalculator
            .CalculateAsync(order, cancellationToken);

        if (!price.IsSuccess)
            Result<bool>.Fail(price.Error);

        order.DeliveryPrice = price.Value;

        order.TotalPrice = order.Subtotal + order.DeliveryPrice;

        return Result<bool>.Success(true);
    }
}
