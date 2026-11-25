using ShagOxServer.Application.Interfaces.Repositories.Orders.OrderItems;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Orders.OrderItems;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Orders;
using ShagOxServer.Domain.Cache.LineItems;
using ShagOxServer.Domain.Cache.Orders;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Orders;
public class OrderInvalidationService
    : ICacheInvalidationService<Order, OrderCacheContext>
{
    private readonly IOrderItemQueryRepository _itemRepository;
    private readonly OrderItemInvalidationService _itemInvalid;

    private readonly IDataCacheService _cache;


    public OrderInvalidationService(
        IOrderItemQueryRepository itemRepository,
        OrderItemInvalidationService itemInvalid,
        IDataCacheService cache)
    {
        _itemRepository = itemRepository;
        _itemInvalid = itemInvalid;
        _cache = cache;
    }


    public async Task InvalidateCreateAsync(
        OrderCacheContext entityInfo)
    {
    }

    public async Task InvalidateDeleteAsync(
       OrderCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);

        var itemInfos = await GetOrderItemInfos(
            entityInfo.Id);

        foreach (var itemInfo in itemInfos)
        {
            await _itemInvalid
                .InvalidateDeleteAsync(itemInfo);
        }
    }

    public async Task InvalidateUpdateAsync(
        OrderCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    private async Task InvalidateAsync(
        OrderCacheContext entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys
           .EntityLanguagePattern<Order>(entityInfo.Id));

        await _cache.RemoveByPatternAsync(OrderCache
          .ByUserPattern(entityInfo.UserId));

        await _cache.RemoveByPatternAsync(OrderCache
          .ByOrderNumberPattern(entityInfo.OrderNumber));

        if (!string.IsNullOrEmpty(entityInfo.PaymentTransactionId))
        {
            await _cache.RemoveByPatternAsync(OrderCache
              .ByPaymentTransactionPattern(entityInfo.PaymentTransactionId));
        }

        if (!string.IsNullOrEmpty(entityInfo.TrackingNumber))
        {
            await _cache.RemoveByPatternAsync(OrderCache
                .ByTrackingNumberPattern(entityInfo.TrackingNumber));
        }
    }

    private async Task<List<LineItemCacheContext>> GetOrderItemInfos(
        long basketId)
    {
        return await _itemRepository
            .GetCacheInfosByParentAsync(basketId);
    }
}
