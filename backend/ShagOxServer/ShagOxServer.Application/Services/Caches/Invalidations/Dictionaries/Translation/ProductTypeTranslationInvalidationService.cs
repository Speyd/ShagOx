using ShagOxServer.Application.DTOs.Base.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Localized;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Translation;
public class ProductTypeTranslationInvalidationService
    : ITranslationCacheInvalidationService<ProductTypeTranslation>
{
    private readonly ICacheService _cache;

    private readonly ProductTypeLocalizedInvalidationService _type;


    public ProductTypeTranslationInvalidationService(
        ProductTypeLocalizedInvalidationService type,
        ICacheService cache)
    {
        _cache = cache;
        _type = type;
    }


    public async Task InvalidateCreateAsync(
      BaseTranslationCacheInfo cacheDto)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
             SearchPattern<ProductTypeTranslation>());
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
             .Entity<ProductTypeTranslation>(cacheDto.Id));

        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityPagedPattern<ProductTypeTranslation>());

        await _cache.RemoveByPatternAsync(CacheKeys.
             SearchPattern<ProductTypeTranslation>());

        await _type.InvalidateLanguageAsync(
            cacheDto.Language);
    }
}