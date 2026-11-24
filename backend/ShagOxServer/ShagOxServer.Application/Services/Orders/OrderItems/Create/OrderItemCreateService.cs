using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.LineItems.Core.Create;
using ShagOxServer.Application.DTOs.LineItems.Core.Query;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Providers.Payment;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Common.Currencies;
using ShagOxServer.Application.Interfaces.Services.Orders.OrderItems.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Resources.Orders.Order;
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

    private readonly ICurrencyConverter _converter;
    private readonly ICurrencyProvider _currencyProvider;
    private readonly ICurrencyQueryRepository _currencyRepository;


    private readonly OrderItemOrderInvalidationService _orderInvalidation;
    private readonly OrderItemInvalidationService _itemInvalidation;

    private readonly IAdvertisementCommandRepository _advertCommand;
    private readonly IAdvertisementVariantCommandRepository _variantCommand;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrderItemCreateService> _logger;

    public OrderItemCreateService(
        IRepository<OrderItem> itemRepository,
        OrderItemValidator itemValidator,
        ICurrencyConverter converter,
        ICurrencyProvider currencyProvider,
        ICurrencyQueryRepository currencyRepository,
        AdvertisementVariantValidator advertVariantValidator,
        AdvertisementValidator advertValidator,
        OrderItemOrderInvalidationService orderInvalidation, 
        IAdvertisementCommandRepository advertCommand,
        IAdvertisementVariantCommandRepository variantCommand,
        OrderItemInvalidationService itemInvalidation,
        IUnitOfWork unitOfWork,
        ILogger<OrderItemCreateService> logger,
        IUserContext userContext
    )
        : base(
            itemValidator,
            userContext,
            advertVariantValidator,
            advertValidator)
    {
        _itemRepository = itemRepository;

        _converter = converter;
        _currencyProvider = currencyProvider;
        _currencyRepository = currencyRepository;

        _orderInvalidation = orderInvalidation;
        _itemInvalidation = itemInvalidation;

        _advertCommand = advertCommand;
        _variantCommand = variantCommand;

        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<OrderItem>> CreateAsync(
        LineItemCreateRequest request,
        CancellationToken cancellationToken = default)
    {
        await _unitOfWork.BeginTransactionAsync();

        OrderItem item;

        try
        {
            var result = await CreateInternalAsync(
                request,
                cancellationToken);

            if (!result.IsSuccess)
            {
                await _unitOfWork.RollbackAsync();

                return Result<OrderItem>.Fail(result.Error);
            }

            item = result.Value!;

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            LogCreationError(ex, request);

            return Result<OrderItem>
                .Fail(EntityErrorResources.OrderItemCreateFailed);
        }

        LogCreationSuccess(item, request);

        await InvalidateItemCacheAsync(item);

        return Result<OrderItem>.Success(item);
    }

    public async Task<Result<OrderItem>> CreateInternalAsync(
        LineItemCreateRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await ValidateAndGetDetailsAsync(
            request,
            cancellationToken);

        if (!validationResult.IsSuccess)
        {
            return Result<OrderItem>
                .Fail(validationResult.Error);
        }

        var details = validationResult.Value!;

        var currency = request.CurrencyId is null ? null 
            : await _currencyRepository
                .GetByIdAsync(request.CurrencyId.Value);

        if (currency is null)
        {
            currency = await _currencyProvider
                .GetCurrencyAsync();

            if (currency is null)
            {
                return Result<OrderItem>.Fail(
                    EntityNamesResources.Currency);
            }
        }


        var (price, code) = await GetPriceAndCurrencyCode(details, request);

        if(!price.HasValue)
        {
            return Result<OrderItem>
                .Fail(OrderResources.InvalidPrice);
        }

        var convertedPrice = currency is null? price:
            _converter.Convert(
                price,
                code,
                currency.Code).ConvertedAmount;

        if (!convertedPrice.HasValue)
        {
            return Result<OrderItem>.Fail(
                OrderResources.InvalidPrice);
        }

        var item = OrderItemCreater.Create(
            request,
            convertedPrice.Value);

        item.TotalWeight = CalculateItemWeight(details, request);
        

        _itemRepository.Add(item);

        item.Advertisement = validationResult.Value?.Advertisement!;
        item.AdvertisementVariant = validationResult.Value?.Variant;

        await _unitOfWork.SaveChangesAsync();


        return Result<OrderItem>.Success(item);
    }

    private async Task<(decimal? Price, string CurrencyCode)>
        GetPriceAndCurrencyCode(
            LineItemDetails details,
            LineItemCreateRequest request)
    {
        string currencyCode = "";

        if (details.Variant is not null)
        {
            currencyCode = details.Variant.Advertisement.Currency.Code;

            await _variantCommand.DecreaseStockAsync(
                details.Variant.Id,
                request.Quantity);

            return (
                details.Price!.Value,
                currencyCode
            );
        }
        else if (details.Advertisement is not null)
        {
            currencyCode = details.Advertisement.Currency.Code;

            await _advertCommand.DecreaseStockAsync(
                details.Advertisement.Id,
                request.Quantity);

            return (
                details.Price!.Value,
                currencyCode
            );
        }

        return (
            null,
            currencyCode
        );
    }

    private static decimal CalculateItemWeight(
        LineItemDetails details,
            LineItemCreateRequest request)
    {
        if (details.Variant is not null)
        {
            return details.Variant.Weight *
                request.Quantity;
        }
        else if (details.Advertisement is not null)
        {
            return details.Advertisement.Weight!.Value *
                request.Quantity;
        }

        return -1;
    }

    private async Task InvalidateItemCacheAsync(OrderItem item)
    {
        var cacheInfo = LineItemCacheMapper.ToInfo(item);

        await _orderInvalidation.InvalidateCreateAsync(cacheInfo);

        await _itemInvalidation.InvalidateCreateAsync(cacheInfo);
    }

    private void LogCreationSuccess(
        OrderItem item,
        LineItemCreateRequest request)
    {
        _logger.LogInformation(
            "Order item created successfully. " +
            "OrderItemId: {OrderItemId}, OrderId: {ParentId}, " +
            "AdvertisementVariantId: {AdvertisementVariantId}",
            item.Id,
            request.ParentId,
            request.AdvertisementVariantId);
    }

    private void LogCreationError(
        Exception ex,
        LineItemCreateRequest request)
    {
        _logger.LogError(
            ex,
            "Failed to create order item. " +
            "OrderId: {ParentId}, AdvertisementVariantId: {AdvertisementVariantId}",
            request.ParentId,
            request.AdvertisementVariantId);
    }
}
