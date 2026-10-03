using ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Baskets;
using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Baskets;
public class BasketAttributeInvalidationService
    : ICacheInvalidationService<BasketAttribute, 
        BasketAttributeCacheInfo>
{
    private readonly ICacheService _cache;


    public BasketAttributeInvalidationService(
        ICacheService cache)
    {
        _cache = cache;
    }


    public async Task InvalidateDeleteAsync(
        BasketAttributeCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys
           .EntityLanguagePattern<BasketAttribute>(entityInfo.Id));

        await _cache.RemoveByPatternAsync(BasketAttributeCache
           .ByCategoryPattern(entityInfo.CategoryId));

        await _cache.RemoveByPatternAsync(BasketAttributeCache
           .ByAttributeDefenitionPattern(entityInfo.AttributeDefinitionId));
    }

    public async Task InvalidateUpdateAsync(
        BasketAttributeCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys
           .EntityLanguagePattern<Advertisement>(entityInfo.Id));

        await _cache.RemoveByPatternAsync(BasketAttributeCache
           .ByCategoryPattern(entityInfo.CategoryId));

        await _cache.RemoveByPatternAsync(BasketAttributeCache
           .ByAttributeDefenitionPattern(entityInfo.AttributeDefinitionId));
    }
}