using ShagOxServer.Application.DTOs.Base.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Localized;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Translation;
public class CategoryTranslationInvalidationService
    : ITranslationCacheInvalidationService<CategoryTranslation>
{
    private readonly ICacheService _cache;

    private readonly CategoryLocalizedInvalidationService _category;


    public CategoryTranslationInvalidationService(
        CategoryLocalizedInvalidationService category,
        ICacheService cache)
    {
        _cache = cache;
        _category = category;
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
             .Entity<CategoryTranslation>(cacheDto.Id));

        await _category.InvalidateLanguageAsync(
            cacheDto.Language);
    }
}