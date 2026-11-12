using ShagOxServer.Application.DTOs.LineItems.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.LineItems;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Baskets.BasketItems;
public class BasketItemInvalidationService
    : ICacheInvalidationService<BasketItem, LineItemCacheInfo>
{ 
    private readonly IDataCacheService _cache;


    public BasketItemInvalidationService(
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
           .EntityLanguagePattern<BasketItem>(entityInfo.Id));

        await _cache.RemoveByPatternAsync(LineItemsCache<BasketItem>
          .ByParentPattern(entityInfo.ParentId));
    }
}
