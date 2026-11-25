using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.LineItems;
using ShagOxServer.Domain.Cache.LineItems;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Orders.OrderItems;
public class OrderItemInvalidationService
    : ICacheInvalidationService<OrderItem, LineItemCacheContext>
{
    private readonly IDataCacheService _cache;


    public OrderItemInvalidationService(
        IDataCacheService cache)
    {
        _cache = cache;
    }


    public async Task InvalidateCreateAsync(
        LineItemCacheContext entityInfo)
    {
    }

    public async Task InvalidateDeleteAsync(
        LineItemCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    public async Task InvalidateUpdateAsync(
        LineItemCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    private async Task InvalidateAsync(
        LineItemCacheContext entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys
           .EntityLanguagePattern<OrderItem>(entityInfo.Id));

        await _cache.RemoveByPatternAsync(LineItemsCache<OrderItem>
          .ByParentPattern(entityInfo.ParentId));
    }
}
