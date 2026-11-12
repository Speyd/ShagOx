using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Baskets.Core.Create;
using ShagOxServer.Application.DTOs.LineItems.Core.Create;
using ShagOxServer.Application.DTOs.Orders.Core.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Create;
using ShagOxServer.Application.Interfaces.Services.Baskets.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Common.Currencies;
using ShagOxServer.Application.Interfaces.Services.Orders.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Orders.OrderItems.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Validator;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Application.Services.Baskets.BasketItems.Create;
using ShagOxServer.Application.Services.Baskets.BasketItems.Validator;
using ShagOxServer.Application.Services.Baskets.Core.Create;
using ShagOxServer.Application.Services.Baskets.Core.Mapping;
using ShagOxServer.Application.Services.Baskets.Core.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets.BasketItems;
using ShagOxServer.Application.Services.Caches.Invalidations.Orders;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Validator;
using ShagOxServer.Application.Services.LineItems.Core.Create;
using ShagOxServer.Application.Services.LineItems.Core.Mapping;
using ShagOxServer.Application.Services.Orders.Core.Mapping;
using ShagOxServer.Application.Services.Specification.Currencies.Validator;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Orders.Core.Create;

public class OrderCreateService
    : IOrderCreateService
{
    private readonly IRepository<Order> _basketRepository;
    private readonly BasketValidator _basketValidator;
    private readonly UserValidator _userValidator;
    private readonly DeliveryProviderValidator _deliveryValidator;
    private readonly CurrencyValidator _currencyValidator;
    private readonly IOrderItemCreateService _orderItemCreateService;
    private readonly OrderInvalidationService _basketInvalidation;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrderCreateService> _logger;


    public OrderCreateService(
        IRepository<Order> basketRepository,
        BasketValidator basketValidator,
        UserValidator userValidator,
        OrderInvalidationService basketInvalidation,
        IUnitOfWork unitOfWork,
        ILogger<OrderCreateService> logger)
    {
        _basketRepository = basketRepository;
        _basketValidator = basketValidator;
        _userValidator = userValidator;
        _basketInvalidation = basketInvalidation;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        OrderCreateRequest request,
        long ownerId,
        CancellationToken cancellationToken = default)
    {
        await _unitOfWork.BeginTransactionAsync();

        var basket = OrderCreater
            .Create(ownerId, request);

        _basketRepository.Add(basket);

        await _unitOfWork.SaveChangesAsync();

        decimal totalPrice = 0m;
        foreach (var item in request.OrderItems)
        {
            var itemResult = await _orderItemCreateService
                .CreateAsync(item with
                {
                    ParentId = item.ParentId
                });

            if (!itemResult.IsSuccess)
            {
                await _unitOfWork.RollbackAsync();

                return Result<CreateResponse>
                    .Fail(itemResult.Error);
            }

            totalPrice += itemResult.Value!.TotalPrice;
        }

        try
        {
            
            await _unitOfWork.CommitAsync();

            await _basketInvalidation.InvalidateCreateAsync(
                OrderCacheMapper.ToInfo(basket));

            return Result<CreateResponse>.Success(
               new CreateResponse(
                   basket.Id,
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

    private async Task<Result<bool>> Validation(
        long ownerId,
        OrderCreateRequest request,
        CancellationToken cancellationToken = default)
    {
        var userExists = await _userValidator
           .ExistsByIdAsync(ownerId, cancellationToken);

        if (!userExists.IsSuccess)
        {
            return Result<bool>
                .Fail(userExists.Error);
        }


        if (request.DeliveryProviderId.HasValue)
        {
            var deliveryExists = await _deliveryValidator.ExistsByIdAsync(
                request.DeliveryProviderId.Value, 
                cancellationToken);

            if (!deliveryExists.IsSuccess)
            {
                return Result<bool>
                    .Fail(deliveryExists.Error);
            }
        }

        var currencyExists = await _currencyValidator
            .ExistsByIdAsync(request.CurrencyId, cancellationToken);

        if (!currencyExists.IsSuccess)
        {
            return Result<bool>
                .Fail(currencyExists.Error);
        }

        return Result<bool>.Success(true);
    }
}
