using ShagOxServer.Application.DTOs.Base.Cache;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements.Translations;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Advertisements.Translations;
public class StatusTranslationInvalidationService
    : ITranslationCacheInvalidationService<StatusTranslation>
{
    private readonly ICacheService _cache;


    public StatusTranslationInvalidationService(
        ICacheService cache)
    {
        _cache = cache;
    }


    public async Task InvalidateDeleteAsync(
        BaseTranslationCacheInfo cacheDto)
    {
        await _cache.RemoveAsync(CacheKeys
            .Entity<StatusTranslation>(cacheDto.Id));

        await _cache.RemoveByPatternAsync(CacheKeys.
            LanguagePattern<Advertisement>(cacheDto.Language));

        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityLanguagePattern<Advertisement>(cacheDto.Language));
    }

    public async Task InvalidateUpdateAsync(
        BaseTranslationCacheInfo cacheDto)
    {
        await _cache.RemoveAsync(CacheKeys
            .Entity<StatusTranslation>(cacheDto.Id));

        await _cache.RemoveByPatternAsync(CacheKeys.
            LanguagePattern<Advertisement>(cacheDto.Language));

        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityLanguagePattern<Advertisement>(cacheDto.Language));
    }
}