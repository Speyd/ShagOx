using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.LineItems.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Orders.OrderItems.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Validator;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.LineItems.Core.Create;
using ShagOxServer.Application.Services.LineItems.Core.Mapping;
using ShagOxServer.Application.Services.Orders.OrderItems.Validator;
using ShagOxServer.Domain.Entities.Orders;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Orders.OrderItems.Create;
public class OrderItemCreateService
    : LineItemCreateService<OrderItem>,
    IOrderItemCreateService
{
    private readonly IRepository<OrderItem> _itemRepository;

    //private readonly BasketItemBasketInvalidationService _basketInvalidation;

   // private readonly BasketItemInvalidationService _itemInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrderItemCreateService> _logger;


    public OrderItemCreateService(
        IRepository<OrderItem> itemRepository,
        OrderItemValidator itemValidator,
        AdvertisementVariantValidator advertVariantValidator,
        AdvertisementValidator advertValidator,
        //BasketItemBasketInvalidationService basketInvalidation,
        //BasketItemInvalidationService itemInvalid,
        IUnitOfWork unitOfWork,
        ILogger<OrderItemCreateService> logger
    )
        : base(itemValidator,
            advertVariantValidator,
            advertValidator)
    {
        _itemRepository = itemRepository;
        //_basketInvalidation = basketInvalidation;
        //_itemInvalid = itemInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        LineItemCreateRequest request,
        CancellationToken cancellationToken = default)
    {
        var validation = await ValidateAndGetDetailsAsync(
            request,
            cancellationToken);

        if (!validation.IsSuccess)
            return Result<CreateResponse>.Fail(validation.Error);

        var item = OrderItemCreater
            .Create(request, validation.Value!.Stock);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _itemRepository.Add(item);

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create order item. " +
                "OrderId: {ParentId}, AdvertisementVariantId: {AdvertisementVariantId}",
                request.ParentId,
                request.AdvertisementVariantId);

            return Result<CreateResponse>
                .Fail(EntityErrorResources.OrderItemCreateFailed);
        }

        _logger.LogInformation(
            "Order item created successfully. " +
            "OrderItemId: {BasketItemId}, OrderId: {ParentId}, " +
            "AdvertisementVariantId: {AdvertisementVariantId}",
            item.Id,
            request.ParentId,
            request.AdvertisementVariantId);

        var caheInfo = LineItemCacheMapper.ToInfo(item);

        //await _basketInvalidation.InvalidateCreateAsync(
        //    caheInfo);

        //await _itemInvalid.InvalidateCreateAsync(
        //    caheInfo);

        return Result<CreateResponse>.Success(
            new CreateResponse(
                item.Id,
                DateTime.UtcNow
        ));
    }
}
