using ShagOxServer.Application.DTOs.Base.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Localized;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Specification.Translations;
public class ConditionTranslationInvalidationService
    : ITranslationCacheInvalidationService<CityTranslation>
{
    private readonly ICacheService _cache;

    private readonly ConditionLocalizedInvalidationService _condition;


    public ConditionTranslationInvalidationService(
        ConditionLocalizedInvalidationService condition,
        ICacheService cache)
    {
        _cache = cache;
        _condition = condition;
    }


    public async Task InvalidateCreateAsync(
     BaseTranslationCacheInfo cacheDto)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
             SearchPattern<CityTranslation>());
    }

    public async Task InvalidateDeleteAsync(
        BaseTranslationCacheInfo cacheDto)
    {
        await InvalidateAsync(cacheDto);
    }

    public async Task InvalidateUpdateAsync(
        BaseTranslationCacheInfo cacheDto)
    {
        await InvalidateAsync(cacheDto);
    }

    private async Task InvalidateAsync(
        BaseTranslationCacheInfo cacheDto)
    {
        await _cache.RemoveAsync(CacheKeys
             .Entity<CityTranslation>(cacheDto.Id));

        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityPagedPattern<CityTranslation>());

        await _cache.RemoveByPatternAsync(CacheKeys.
             SearchPattern<CityTranslation>());

        await _condition.InvalidateLanguageAsync(
            cacheDto.Language);
    }
}