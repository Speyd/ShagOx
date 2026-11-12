using ShagOxServer.Application.DTOs.LineItems.LineItemAttributes.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.LineItems;
using ShagOxServer.Domain.Entities.LineItems;

namespace ShagOxServer.Application.Services.Caches.Invalidations.LineItems;
public class LineItemAttributeInvalidationService
    : ICacheInvalidationService<LineItemAttribute,
        LineItemAttributeCacheInfo>
{
    private readonly IDataCacheService _cache;


    public LineItemAttributeInvalidationService(
        IDataCacheService cache)
    {
        _cache = cache;
    }


    public async Task InvalidateCreateAsync(
        LineItemAttributeCacheInfo entityInfo)
    {
        await InvalidatePatternAsync(entityInfo);
    }

    public async Task InvalidateDeleteAsync(
        LineItemAttributeCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    public async Task InvalidateUpdateAsync(
        LineItemAttributeCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    private async Task InvalidateAsync(
        LineItemAttributeCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys
           .EntityLanguagePattern<LineItemAttribute>(entityInfo.Id));

        await InvalidatePatternAsync(entityInfo);
    }

    private async Task InvalidatePatternAsync(
        LineItemAttributeCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
            LanguageSearchPattern<LineItemAttribute>(
               entityInfo.Status));

        await _cache.RemoveByPatternAsync(CacheKeys.
             EntityLanguagePagedPattern<LineItemAttribute>(
                entityInfo.Status));

        await _cache.RemoveByPatternAsync(LineItemAttributeCache
           .ByCategoryPattern(entityInfo.CategoryId));

        await _cache.RemoveByPatternAsync(LineItemAttributeCache
           .ByAttributeDefenitionPattern(entityInfo.AttributeDefinitionId));
    }
}
