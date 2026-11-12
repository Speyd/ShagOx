using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.LineItems.Core.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Common.Currencies;
using ShagOxServer.Application.Interfaces.Services.Orders.OrderItems.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Resources.Order;
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

    private readonly OrderItemOrderInvalidationService _orderInvalidation;
    private readonly OrderItemInvalidationService _itemInvalidation;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrderItemCreateService> _logger;

    public OrderItemCreateService(
        IRepository<OrderItem> itemRepository,
        OrderItemValidator itemValidator,
        ICurrencyConverter converter,
        ICurrencyProvider currencyProvider,
        AdvertisementVariantValidator advertVariantValidator,
        AdvertisementValidator advertValidator,
        OrderItemOrderInvalidationService orderInvalidation,
        OrderItemInvalidationService itemInvalidation,
        IUnitOfWork unitOfWork,
        ILogger<OrderItemCreateService> logger)
        : base(
            itemValidator,
            advertVariantValidator,
            advertValidator)
    {
        _itemRepository = itemRepository;

        _converter = converter;
        _currencyProvider = currencyProvider;

        _orderInvalidation = orderInvalidation;
        _itemInvalidation = itemInvalidation;

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

        var currency = await _currencyProvider.GetCurrencyAsync();

        if (currency is null)
        {
            return Result<OrderItem>.NotFound(
                EntityNamesResources.User);
        }

        var (price, code) = GetPriceAndCurrencyCode(details);

        var convertedPrice = _converter.Convert(
            price,
            code,
            currency.Code);

        if (!convertedPrice.ConvertedAmount.HasValue)
        {
            return Result<OrderItem>.Fail(
                OrderResources.InvalidPrice);
        }

        var item = OrderItemCreater.Create(
            request,
            convertedPrice.ConvertedAmount.Value);

        item.TotalWeight = CalculateItemWeight(details);

        _itemRepository.Add(item);

        return Result<OrderItem>.Success(item);
    }

    private static (decimal Price, string CurrencyCode)
        GetPriceAndCurrencyCode(dynamic details)
    {
        if (details.Variant is not null)
        {
            details.Variant.Stock -= details.Stock;

            return (
                details.Price,
                details.Variant.Advertisement.Currency.Code
            );
        }

        details.Advertisement.Stock -= details.Stock;

        return (
            details.Price,
            details.Advertisement.Currency.Code
        );
    }

    private static decimal CalculateItemWeight(dynamic details)
    {
        if (details.Variant is not null)
        {
            return details.Variant.Weight;
        }

        return details.Advertisement.Weight ?? 0;
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
