using ShagOxServer.Application.DTOs.Base.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Location.Localized;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Location.Translation;
public class RegionTranslationInvalidationService
    : ITranslationCacheInvalidationService<RegionTranslation>
{
    private readonly ICacheService _cache;

    private readonly RegionLocalizedInvalidationService _region;


    public RegionTranslationInvalidationService(
        RegionLocalizedInvalidationService region,
        ICacheService cache)
    {
        _cache = cache;
        _region = region;
    }


    public async Task InvalidateCreateAsync(
      BaseTranslationCacheInfo cacheDto)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
             SearchPattern<RegionTranslation>());
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
             .Entity<RegionTranslation>(cacheDto.Id));

        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityPagedPattern<RegionTranslation>());

        await _cache.RemoveByPatternAsync(CacheKeys.
             SearchPattern<RegionTranslation>());

        await _region.InvalidateLanguageAsync(
            cacheDto.Language);
    }
}