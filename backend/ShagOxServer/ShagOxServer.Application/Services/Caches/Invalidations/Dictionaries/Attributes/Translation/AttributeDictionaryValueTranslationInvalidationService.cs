using ShagOxServer.Application.DTOs.Base.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes.Localized;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes.Translation;
public class AttributeDictionaryValueTranslationInvalidationService
    : ITranslationCacheInvalidationService
        <AttributeDictionaryValueTranslation>
{
    private readonly ICacheService _cache;

    private readonly AttributeDictionaryValueLocalizedInvalidationService _value;


    public AttributeDictionaryValueTranslationInvalidationService(
        AttributeDictionaryValueLocalizedInvalidationService value,
        ICacheService cache)
    {
        _cache = cache;
        _value = value;
    }


    public async Task InvalidateCreateAsync(
       BaseTranslationCacheInfo cacheDto)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
             SearchPattern<AttributeDictionaryValueTranslation>());
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
             .Entity<AttributeDictionaryValueTranslation>
                (cacheDto.Id));

        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityPagedPattern<AttributeDictionaryValueTranslation>());

        await _cache.RemoveByPatternAsync(CacheKeys.
             SearchPattern<AttributeDictionaryValueTranslation>());

        await _value.InvalidateLanguageAsync(
            cacheDto.Language);
    }
}