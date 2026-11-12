using Microsoft.Extensions.Logging;
using Npgsql.Internal;
using ShagOxServer.Application.DTOs.LineItems.Core.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Common.Currencies;
using ShagOxServer.Application.Interfaces.Services.Orders.OrderItems.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Resources.Order;
using ShagOxServer.Application.Resources.Validations;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Validator;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Orders.OrderItems;
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

    private readonly OrderItemOrderInvalidationService _orderInvalidation;

    private readonly ICurrencyConverter _converter;
    private readonly ICurrencyProvider _currencyProvider;


    private readonly OrderItemInvalidationService _itemInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrderItemCreateService> _logger;


    public OrderItemCreateService(
        IRepository<OrderItem> itemRepository,
        OrderItemValidator itemValidator,
        ICurrencyConverter converter,
        AdvertisementVariantValidator advertVariantValidator,
        AdvertisementValidator advertValidator,
        OrderItemOrderInvalidationService orderInvalidation,
        OrderItemInvalidationService itemInvalid,
        IUnitOfWork unitOfWork,
        ILogger<OrderItemCreateService> logger
    )
        : base(itemValidator,
            advertVariantValidator,
            advertValidator)
    {
        _itemRepository = itemRepository;
        _converter = converter;
        _orderInvalidation = orderInvalidation;
        _itemInvalid = itemInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<OrderItem>> CreateAsync(
        LineItemCreateRequest request,
        CancellationToken cancellationToken = default)
    {
        var validation = await ValidateAndGetDetailsAsync(
            request,
            cancellationToken);

        if (!validation.IsSuccess)
            return Result<OrderItem>.Fail(validation.Error);

        var currency = await _currencyProvider
            .GetCurrencyAsync();

        if(currency is null)
        {
            return Result<OrderItem>.NotFound(
                EntityNamesResources.User);
        }


        string code = "";
        if (validation.Value!.Variant is not null)
        {
            validation.Value.Variant.Stock -= validation.Value!.Stock;
            code = validation.Value.Variant.Advertisement.Currency.Code;
        }
        else if (validation.Value!.Advertisement is not null)
        {
            validation.Value.Advertisement.Stock -= validation.Value!.Stock;
            code = validation.Value.Advertisement.Currency.Code;

        }

        var newPrice = _converter.Convert(
                validation.Value!.Price,
                code,
                currency.Code);

        if(!newPrice.ConvertedAmount.HasValue)
        {
            return Result<OrderItem>.Fail(
                OrderResources.InvalidPrice);
        }

        var item = OrderItemCreater.Create(
            request, 
            newPrice.ConvertedAmount.Value);

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

            return Result<OrderItem>
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

        await _orderInvalidation.InvalidateCreateAsync(
            caheInfo);

        await _itemInvalid.InvalidateCreateAsync(
            caheInfo);

        return Result<OrderItem>.Success(item);
    }
}
