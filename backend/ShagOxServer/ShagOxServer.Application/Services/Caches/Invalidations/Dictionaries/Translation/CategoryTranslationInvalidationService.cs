using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Localized;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Cache.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Translation;
public class CategoryTranslationInvalidationService
    : ITranslationCacheInvalidationService<CategoryTranslation>
{
    private readonly IDataCacheService _cache;

    private readonly CategoryLocalizedInvalidationService _category;


    public CategoryTranslationInvalidationService(
        CategoryLocalizedInvalidationService category,
        IDataCacheService cache)
    {
        _cache = cache;
        _category = category;
    }


    public async Task InvalidateCreateAsync(
        TranslationCacheContext cacheDto)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
             SearchPattern<CategoryTranslation>());

        await _cache.RemoveByPatternAsync(CacheKeys.
           EntityPagedPattern<CategoryTranslation>());
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
             .Entity<CategoryTranslation>(cacheDto.Id));

        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityPagedPattern<CategoryTranslation>());

        await _cache.RemoveByPatternAsync(CacheKeys.
             SearchPattern<CategoryTranslation>());

        await _category.InvalidateLanguageAsync(
            cacheDto.Language);
    }
}
