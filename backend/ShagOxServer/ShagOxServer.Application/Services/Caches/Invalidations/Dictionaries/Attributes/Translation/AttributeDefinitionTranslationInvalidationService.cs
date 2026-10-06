using ShagOxServer.Application.DTOs.Base.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes.Localized;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes.Translation;
public class AttributeDefinitionTranslationInvalidationService
    : ITranslationCacheInvalidationService<AttributeDefinitionTranslation>
{
    private readonly ICacheService _cache;

    private readonly AttributeDefinitionLocalizedInvalidationService _attribute;


    public AttributeDefinitionTranslationInvalidationService(
        AttributeDefinitionLocalizedInvalidationService attribute,
        ICacheService cache)
    {
        _cache = cache;
        _attribute = attribute;
    }


    public async Task InvalidateCreateAsync(
       BaseTranslationCacheInfo cacheDto)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
             SearchPattern<AttributeDefinitionTranslation>());
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
             .Entity<AttributeDefinitionTranslation>(cacheDto.Id));

        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityPagedPattern<AttributeDefinitionTranslation>());

        await _cache.RemoveByPatternAsync(CacheKeys.
             SearchPattern<AttributeDefinitionTranslation>());

        await _attribute.InvalidateLanguageAsync(
            cacheDto.Language);
    }
}