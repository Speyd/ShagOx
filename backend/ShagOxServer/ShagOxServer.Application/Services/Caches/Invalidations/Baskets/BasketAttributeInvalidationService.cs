using ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Baskets;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Baskets;
public class BasketAttributeInvalidationService
    : ICacheInvalidationService<BasketAttribute, 
        BasketAttributeCacheInfo>
{
    private readonly IDataCacheService _cache;


    public BasketAttributeInvalidationService(
        IDataCacheService cache)
    {
        _cache = cache;
    }


    public async Task InvalidateCreateAsync(
        BasketAttributeCacheInfo entityInfo)
    {
        await InvalidatePatternAsync(entityInfo);
    }

    public async Task InvalidateDeleteAsync(
        BasketAttributeCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    public async Task InvalidateUpdateAsync(
        BasketAttributeCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    private async Task InvalidateAsync(
        BasketAttributeCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys
           .EntityLanguagePattern<BasketAttribute>(entityInfo.Id,
                entityInfo.Status));

        await InvalidatePatternAsync(entityInfo);
    }

    private async Task InvalidatePatternAsync(
        BasketAttributeCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
            LanguageSearchPattern<BasketAttribute>(
               entityInfo.Status));

        await _cache.RemoveByPatternAsync(CacheKeys.
             EntityLanguagePagedPattern<BasketAttribute>(
                entityInfo.Status));

        await _cache.RemoveByPatternAsync(BasketAttributeCache
           .ByCategoryPattern(entityInfo.CategoryId,
                entityInfo.Status));

        await _cache.RemoveByPatternAsync(BasketAttributeCache
           .ByAttributeDefenitionPattern(entityInfo.AttributeDefinitionId,
                entityInfo.Status));
    }
}
