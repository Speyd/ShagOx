using ShagOxServer.Application.DTOs.Advertisements.Core.Cache;
using ShagOxServer.Application.DTOs.Base.Cache;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Translation;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries;
public class CategoryInvalidationService
    : ICacheInvalidationService<Category, CategoryCacheInfo>
{

    private readonly IAttributeDefinitionQueryRepository _attributeRepository;
    private readonly AttributeDefinitionInvalidationService _attributeInvalid;

    private readonly IAdvertisementQueryRepository _advertRepository;
    private readonly AdvertisementInvalidationService _advertInvalid;

    private readonly ICategoryTranslationQueryRepository _transRepository;
    private readonly CategoryTranslationInvalidationService _transInvalid;

    private readonly ICacheService _cache;



    public CategoryInvalidationService(
        IAttributeDefinitionQueryRepository attributeRepository,
        AttributeDefinitionInvalidationService attributeInvalid,
        IAdvertisementQueryRepository advertRepository,
        AdvertisementInvalidationService advertInvalid,
        ICategoryTranslationQueryRepository transRepository,
        CategoryTranslationInvalidationService transInvalid,
        ICacheService cache)
    {
        _attributeRepository = attributeRepository;
        _attributeInvalid = attributeInvalid;
        _advertRepository = advertRepository;
        _advertInvalid = advertInvalid;
        _transRepository = transRepository;
        _transInvalid = transInvalid;
        _cache = cache;
    }


    public async Task InvalidateCreateAsync(
       CategoryCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
             LanguageSearchPattern<Category>());
    }

    public async Task InvalidateDeleteAsync(
        CategoryCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);


        var attributeIds =
            await GetAttributeDefinitionIds(entityInfo.Id);

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
    }

    public async Task InvalidateUpdateAsync(
        CategoryCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);


        var attributeIds =
            await GetAttributeDefinitionIds(entityInfo.Id);

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
        CategoryCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityLanguagePattern<Category>(entityInfo.Id));

        await _cache.RemoveByPatternAsync(CategoryCache.
            ByProductTypePattern(entityInfo.ProductTypeId));

        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityLanguagePagedPattern<Category>());

        await _cache.RemoveByPatternAsync(CacheKeys.
             LanguageSearchPattern<Category>());
    }

    private async Task<List<long>> GetAttributeDefinitionIds(
        long entityId)
    {
        return await _attributeRepository
            .GetIdsByAttributeDictionaryAsync(entityId);
    }

    private async Task<List<AdvertisementCacheInfo>> GetAdvertisementInfos(
        long entityId)
    {
        return await _advertRepository
            .GetCacheInfosByCategoryAsync(entityId);
    }

    private async Task<List<BaseTranslationCacheInfo>> GetTranslationInfos(
        long entityId)
    {
        return await _transRepository
            .GetCacheInfosByTranslatableAsync(entityId);
    }
}