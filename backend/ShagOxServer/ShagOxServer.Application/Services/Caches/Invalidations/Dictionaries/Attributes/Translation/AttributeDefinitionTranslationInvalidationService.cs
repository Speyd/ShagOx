using ShagOxServer.Application.DTOs.Base.Cache;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes.Translation;
public class AttributeDefinitionTranslationInvalidationService
    : ITranslationCacheInvalidationService<AttributeDefinitionTranslation>
{
    private readonly ICacheService _cache;


    public AttributeDefinitionTranslationInvalidationService(
        ICacheService cache)
    {
        _cache = cache;
    }


    public async Task InvalidateDeleteAsync(
        BaseTranslationCacheInfo cacheDto)
    {
        await _cache.RemoveAsync(CacheKeys
            .Entity<AttributeDefinitionTranslation>(cacheDto.Id));

        await _cache.RemoveByPatternAsync(CacheKeys.
           LanguagePattern<BasketAttribute>(cacheDto.Language));

        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityLanguagePattern<BasketAttribute>(cacheDto.Language));
    }

    public async Task InvalidateUpdateAsync(
        BaseTranslationCacheInfo cacheDto)
    {
        await _cache.RemoveAsync(CacheKeys
            .Entity<AttributeDefinitionTranslation>(cacheDto.Id));

        await _cache.RemoveByPatternAsync(CacheKeys.
           LanguagePattern<BasketAttribute>(cacheDto.Language));

        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityLanguagePattern<BasketAttribute>(cacheDto.Language));
    }
}