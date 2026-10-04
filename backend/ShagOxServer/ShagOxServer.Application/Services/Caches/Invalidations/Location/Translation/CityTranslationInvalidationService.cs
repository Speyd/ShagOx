using ShagOxServer.Application.DTOs.Base.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Location.Localized;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Location.Translation;
public class CityTranslationInvalidationService
    : ITranslationCacheInvalidationService<CityTranslation>
{
    private readonly ICacheService _cache;

    private readonly CityLocalizedInvalidationService _city;


    public CityTranslationInvalidationService(
        CityLocalizedInvalidationService city,
        ICacheService cache)
    {
        _cache = cache;
        _city = city;
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

        await _city.InvalidateLanguageAsync(
            cacheDto.Language);
    }
}