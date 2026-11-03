using ShagOxServer.Application.DTOs.Base.Cache;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Cache;
using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Translation;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries;
public class ProductTypeInvalidationService
    : ICacheInvalidationService<ProductType, ProductTypeCacheInfo>
{
    private readonly ICategoryQueryRepository _categoryRepository;
    private readonly CategoryInvalidationService _categoryInvalid;

    private readonly IProductTypeTranslationQueryRepository _transRepository;
    private readonly ProductTypeTranslationInvalidationService _transInvalid;

    private readonly IDataCacheService _cache;



    public ProductTypeInvalidationService(
        ICategoryQueryRepository categoryRepository,
        CategoryInvalidationService categoryInvalid,
        IProductTypeTranslationQueryRepository transRepository,
        ProductTypeTranslationInvalidationService transInvalid,
        IDataCacheService cache)
    {
        _categoryRepository = categoryRepository;
        _categoryInvalid = categoryInvalid;
        _transRepository = transRepository;
        _transInvalid = transInvalid;
        _cache = cache;
    }


    public async Task InvalidateCreateAsync(
       ProductTypeCacheInfo entityInfo)
    {
        await InvalidatePatternAsync(entityInfo);
    }

    public async Task InvalidateDeleteAsync(
        ProductTypeCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);


        var categoryInfos =
            await GetCategoryInfos(entityInfo.Id);

        foreach (var categoryInfo in categoryInfos)
        {
            await _categoryInvalid
                .InvalidateDeleteAsync(categoryInfo);
        }


        var transInfos =
           await GetTranslationInfos(entityInfo.Id);

        foreach (var transInfo in transInfos)
        {
            await _transInvalid
                .InvalidateDeleteAsync(transInfo);
        }
    }

    public async Task InvalidateUpdateAsync(
        ProductTypeCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);


        var categoryInfos =
             await GetCategoryInfos(entityInfo.Id);

        foreach (var categoryInfo in categoryInfos)
        {
            await _categoryInvalid
                .InvalidateUpdateAsync(categoryInfo);
        }
    }

    private async Task InvalidateAsync(
        ProductTypeCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityLanguagePattern<ProductType>(entityInfo.Id,
                entityInfo.Status));

        await InvalidatePatternAsync(entityInfo);
    }

    private async Task InvalidatePatternAsync(
        ProductTypeCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityLanguagePagedPattern<ProductType>(
                entityInfo.Status));

        await _cache.RemoveByPatternAsync(CacheKeys.
             LanguageSearchPattern<ProductType>(
                entityInfo.Status));
    }

    private async Task<List<CategoryCacheInfo>> GetCategoryInfos(
        long entityId)
    {
        return await _categoryRepository
            .GetCacheInfosByProductTypeAsync(entityId);
    }

    private async Task<List<BaseTranslationCacheInfo>> GetTranslationInfos(
        long entityId)
    {
        return await _transRepository
            .GetCacheInfosByTranslatableAsync(entityId);
    }
}
