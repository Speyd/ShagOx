using ShagOxServer.Application.DTOs.Base.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements.Localized;
using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements.Translations;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Advertisements.Translations;
public class StatusTranslationInvalidationService
    : ITranslationCacheInvalidationService<StatusTranslation>
{
    private readonly ICacheService _cache;
    private readonly StatusLocalizedInvalidationService _status;

    public StatusTranslationInvalidationService(
        StatusLocalizedInvalidationService status,
        ICacheService cache)
    {
        _cache = cache;
        _status = status;
    }


    public async Task InvalidateDeleteAsync(
        BaseTranslationCacheInfo cacheDto)
    {
        await _cache.RemoveAsync(CacheKeys
            .Entity<StatusTranslation>(cacheDto.Id));

        await _status.InvalidateLanguageAsync(
            cacheDto.Language);
    }

    public async Task InvalidateUpdateAsync(
        BaseTranslationCacheInfo cacheDto)
    {
        await _cache.RemoveAsync(CacheKeys
            .Entity<StatusTranslation>(cacheDto.Id));

        await _status.InvalidateLanguageAsync(
            cacheDto.Language);
    }
}