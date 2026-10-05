using ShagOxServer.Application.DTOs.Baskets.BasketItems.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Baskets;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Baskets.BasketItems;
public class BasketItemInvalidationService
    : ICacheInvalidationService<BasketItem, BasketItemCacheInfo>
{ 
    private readonly ICacheService _cache;


    public BasketItemInvalidationService(
        ICacheService cache)
    {
        _cache = cache;
    }

    public async Task InvalidateDeleteAsync(
        BasketItemCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    public async Task InvalidateUpdateAsync(
        BasketItemCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    private async Task InvalidateAsync(
        BasketItemCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys
           .EntityLanguagePattern<BasketAttribute>(entityInfo.Id));

        await _cache.RemoveByPatternAsync(BasketItemCache
          .ByBasketPattern(entityInfo.BasketId));

        await _cache.RemoveByPatternAsync(BasketItemCache
          .ByAdvertisementVariantPattern(entityInfo.AdvertVariantId));
    }
}