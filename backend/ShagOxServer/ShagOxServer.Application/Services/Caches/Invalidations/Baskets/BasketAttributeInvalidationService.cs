using ShagOxServer.Application.DTOs.Advertisements.Core.Cache;
using ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Baskets;
using ShagOxServer.Domain.Entities.Account;
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


    public async Task InvalidateCreateAsync(
        BasketAttributeCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
             LanguageSearchPattern<BasketAttribute>());
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
           .EntityLanguagePattern<BasketAttribute>(entityInfo.Id));

        await _cache.RemoveByPatternAsync(BasketAttributeCache
           .ByCategoryPattern(entityInfo.CategoryId));

        await _cache.RemoveByPatternAsync(BasketAttributeCache
           .ByAttributeDefenitionPattern(entityInfo.AttributeDefinitionId));

        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityLanguagePagedPattern<BasketAttribute>());

        await _cache.RemoveByPatternAsync(CacheKeys.
             LanguageSearchPattern<BasketAttribute>());
    }
}