using ShagOxServer.Application.DTOs.LineItems.Core.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.LineItems;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Orders.OrderItems;
public class OrderItemInvalidationService
    : ICacheInvalidationService<OrderItem, LineItemCacheInfo>
{
    private readonly IDataCacheService _cache;


    public OrderItemInvalidationService(
        IDataCacheService cache)
    {
        _cache = cache;
    }


    public async Task InvalidateCreateAsync(
        LineItemCacheInfo entityInfo)
    {
    }

    public async Task InvalidateDeleteAsync(
        LineItemCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    public async Task InvalidateUpdateAsync(
        LineItemCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    private async Task InvalidateAsync(
        LineItemCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys
           .EntityLanguagePattern<OrderItem>(entityInfo.Id));

        await _cache.RemoveByPatternAsync(LineItemsCache<OrderItem>
          .ByParentPattern(entityInfo.ParentId));
    }
}
