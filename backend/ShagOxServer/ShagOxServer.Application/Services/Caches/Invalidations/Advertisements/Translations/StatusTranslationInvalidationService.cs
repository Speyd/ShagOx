using ShagOxServer.Application.DTOs.Base.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements.Localized;
using ShagOxServer.Application.Services.Caches.Keys;
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


    public async Task InvalidateCreateAsync(
       BaseTranslationCacheInfo cacheDto)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
             SearchPattern<StatusTranslation>());
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
            .Entity<StatusTranslation>(cacheDto.Id));

        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityPagedPattern<StatusTranslation>());

        await _cache.RemoveByPatternAsync(CacheKeys.
             SearchPattern<StatusTranslation>());

        await _status.InvalidateLanguageAsync(
            cacheDto.Language);
    }
}