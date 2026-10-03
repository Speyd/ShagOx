using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Baskets;
public class BasketItemInvalidationService
    : ICacheInvalidationService<BasketItem, long>
{ 
    private readonly ICacheService _cache;


    public BasketItemInvalidationService(
        ICacheService cache)
    {
        _cache = cache;
    }


    public async Task InvalidateDeleteAsync(
        long entityCacheId)
    {
        await _cache.RemoveByPatternAsync(CacheKeys
           .EntityLanguagePattern<BasketAttribute>(entityCacheId));
    }

    public async Task InvalidateUpdateAsync(
        long entityCacheId)
    {
        await _cache.RemoveByPatternAsync(CacheKeys
           .EntityLanguagePattern<BasketAttribute>(entityCacheId));
    }
}