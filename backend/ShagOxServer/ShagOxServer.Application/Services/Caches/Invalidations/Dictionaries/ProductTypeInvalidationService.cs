using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Translation;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Cache.Dictionaries;
using ShagOxServer.Domain.Cache.Translations;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries;
public class ProductTypeInvalidationService
    : ICacheInvalidationService<ProductType, ProductTypeCacheContext>
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
       ProductTypeCacheContext entityInfo)
    {
        await InvalidatePatternAsync(entityInfo);
    }

    public async Task InvalidateDeleteAsync(
        ProductTypeCacheContext entityInfo)
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
        ProductTypeCacheContext entityInfo)
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
        ProductTypeCacheContext entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityLanguagePattern<ProductType>(entityInfo.Id));

        await InvalidatePatternAsync(entityInfo);
    }

    private async Task InvalidatePatternAsync(
        ProductTypeCacheContext entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityLanguagePagedPattern<ProductType>(
                entityInfo.Status));

        await _cache.RemoveByPatternAsync(CacheKeys.
             LanguageSearchPattern<ProductType>(
                entityInfo.Status));
    }

    private async Task<List<CategoryCacheContext>> GetCategoryInfos(
        long entityId)
    {
        return await _categoryRepository
            .GetCacheInfosByProductTypeAsync(entityId);
    }

    private async Task<List<TranslationCacheContext>> GetTranslationInfos(
        long entityId)
    {
        return await _transRepository
            .GetCacheInfosByTranslatableAsync(entityId);
    }
}
