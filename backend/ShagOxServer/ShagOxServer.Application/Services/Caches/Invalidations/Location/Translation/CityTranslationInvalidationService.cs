using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Location.Localized;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Cache.Translations;
using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Location.Translation;
public class CityTranslationInvalidationService
    : ITranslationCacheInvalidationService<CityTranslation>
{
    private readonly IDataCacheService _cache;

    private readonly CityLocalizedInvalidationService _city;


    public CityTranslationInvalidationService(
        CityLocalizedInvalidationService city,
        IDataCacheService cache)
    {
        _cache = cache;
        _city = city;
    }


    public async Task InvalidateCreateAsync(
        TranslationCacheContext cacheDto)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
             SearchPattern<CityTranslation>());

        await _cache.RemoveByPatternAsync(CacheKeys.
             EntityPagedPattern<CityTranslation>());
    }

    public async Task InvalidateDeleteAsync(
        TranslationCacheContext cacheDto)
    {
        await InvalidateAsync(cacheDto);
    }

    public async Task InvalidateUpdateAsync(
        TranslationCacheContext cacheDto)
    {
        await InvalidateAsync(cacheDto);
    }

    private async Task InvalidateAsync(
        TranslationCacheContext cacheDto)
    {
        await _cache.RemoveAsync(CacheKeys
             .Entity<CityTranslation>(cacheDto.Id));

        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityPagedPattern<CityTranslation>());

        await _cache.RemoveByPatternAsync(CacheKeys.
             SearchPattern<CityTranslation>());

        await _city.InvalidateLanguageAsync(
            cacheDto.Language);
    }
}
