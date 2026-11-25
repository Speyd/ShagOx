using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Translations;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.CategoryImages;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Translation;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Dictionaries;
using ShagOxServer.Domain.Cache.Advertisements;
using ShagOxServer.Domain.Cache.Dictionaries;
using ShagOxServer.Domain.Cache.Dictionaries.Attributes;
using ShagOxServer.Domain.Cache.Specification;
using ShagOxServer.Domain.Cache.Translations;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries;
public class CategoryInvalidationService
    : ICacheInvalidationService<Category, CategoryCacheContext>
{

    private readonly IAttributeDefinitionQueryRepository _attributeRepository;
    private readonly AttributeDefinitionInvalidationService _attributeInvalid;

    private readonly IAdvertisementQueryRepository _advertRepository;
    private readonly AdvertisementInvalidationService _advertInvalid;

    private readonly ICategoryTranslationQueryRepository _transRepository;
    private readonly CategoryTranslationInvalidationService _transInvalid;

    private readonly ICategoryImageQueryRepository _imageRepository;
    private readonly CategoryImageInvalidationService _imageInvalid;

    private readonly IDataCacheService _cache;



    public CategoryInvalidationService(
        IAttributeDefinitionQueryRepository attributeRepository,
        AttributeDefinitionInvalidationService attributeInvalid,
        IAdvertisementQueryRepository advertRepository,
        AdvertisementInvalidationService advertInvalid,
        ICategoryTranslationQueryRepository transRepository,
        CategoryTranslationInvalidationService transInvalid,
        ICategoryImageQueryRepository imageRepository,
        CategoryImageInvalidationService imageInvalid,
        IDataCacheService cache)
    {
        _attributeRepository = attributeRepository;
        _attributeInvalid = attributeInvalid;
        _advertRepository = advertRepository;
        _advertInvalid = advertInvalid;
        _transRepository = transRepository;
        _transInvalid = transInvalid;
        _imageRepository = imageRepository;
        _imageInvalid = imageInvalid;
        _cache = cache;
    }


    public async Task InvalidateCreateAsync(
       CategoryCacheContext entityInfo)
    {
        await InvalidatePatternAsync(entityInfo);
    }

    public async Task InvalidateDeleteAsync(
        CategoryCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);


        var attributeIds = await GetAttributeDefinitionCacheInfos(
            entityInfo.Id);

        foreach (var attributeId in attributeIds)
        {
            await _attributeInvalid
                .InvalidateDeleteAsync(attributeId);
        }


        var advertInfos =
            await GetAdvertisementInfos(entityInfo.Id);

        foreach (var advertInfo in advertInfos)
        {
            await _advertInvalid
                .InvalidateDeleteAsync(advertInfo);
        }


        var transInfos =
           await GetTranslationInfos(entityInfo.Id);

        foreach (var transInfo in transInfos)
        {
            await _transInvalid
                .InvalidateDeleteAsync(transInfo);
        }

        var imageInfo = await GetImageInfo(
            entityInfo.Id);

        if (imageInfo is not null)
        {
            await _imageInvalid
                .InvalidateDeleteAsync(imageInfo);
        }
    }

    public async Task InvalidateUpdateAsync(
        CategoryCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);


        var attributeIds =
            await GetAttributeDefinitionCacheInfos(entityInfo.Id);

        foreach (var attributeId in attributeIds)
        {
            await _attributeInvalid
                .InvalidateUpdateAsync(attributeId);
        }


        var advertInfos =
            await GetAdvertisementInfos(entityInfo.Id);

        foreach (var advertInfo in advertInfos)
        {
            await _advertInvalid
                .InvalidateUpdateAsync(advertInfo);
        }
    }

    private async Task InvalidateAsync(
        CategoryCacheContext entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityLanguagePattern<Category>(entityInfo.Id));

        await InvalidatePatternAsync(entityInfo);
    }

    private async Task InvalidatePatternAsync(
        CategoryCacheContext entityInfo)
    {
        await _cache.RemoveByPatternAsync(CategoryCache.
            ByProductTypePattern(entityInfo.ProductTypeId));

        await _cache.RemoveByPatternAsync(CacheKeys.
             LanguageSearchPattern<Category>(
                entityInfo.Status));

        await _cache.RemoveByPatternAsync(CacheKeys.
           EntityLanguagePagedPattern<Category>(
                entityInfo.Status));
    }

    private async Task<List<AttributeDefinitionCacheContext>> 
        GetAttributeDefinitionCacheInfos(
        long entityId)
    {
        return await _attributeRepository
            .GetCacheInfosByAttributeDictionaryAsync(entityId);
    }

    private async Task<List<AdvertisementCacheContext>> GetAdvertisementInfos(
        long entityId)
    {
        return await _advertRepository
            .GetCacheInfosByCategoryAsync(entityId);
    }

    private async Task<List<TranslationCacheContext>> GetTranslationInfos(
        long entityId)
    {
        return await _transRepository
            .GetCacheInfosByTranslatableAsync(entityId);
    }

    private async Task<PictureCacheContext?> GetImageInfo(
       long entityId)
    {
        return await _imageRepository
            .GetCacheInfoByOwnerAsync(entityId);
    }
}
