using ShagOxServer.Application.DTOs.Base.Cache;
using ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Cache;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes.Translation;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes;
public class AttributeDefinitionInvalidationService
    : ICacheInvalidationService<AttributeDefinition, 
        AttributeDefinitionCacheInfo>
{
    private readonly IBasketAttributeQueryRepository _basketRepository;

    private readonly BasketAttributeInvalidationService _basketInvalid;

    private readonly IAttributeDefinitionTranslationQueryRepository _transRepository;

    private readonly AttributeDefinitionTranslationInvalidationService _transInvalid;

    private readonly IDataCacheService _cache;



    public AttributeDefinitionInvalidationService(
        IBasketAttributeQueryRepository basketRepository,
        BasketAttributeInvalidationService basketInvalid,
        IAttributeDefinitionTranslationQueryRepository transRepository,
        AttributeDefinitionTranslationInvalidationService transInvalid,
        IDataCacheService cache)
    {
        _basketRepository = basketRepository;
        _basketInvalid = basketInvalid;
        _transRepository = transRepository;
        _transInvalid = transInvalid;
        _cache = cache;
    }


    public async Task InvalidateCreateAsync(
        AttributeDefinitionCacheInfo entityInfo)
    {
        await InvalidatePatternAsync(entityInfo);
    }

    public async Task InvalidateDeleteAsync(
        AttributeDefinitionCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);

        var translationInfos =
            await GetTranslationInfos(entityInfo.Id);

        foreach (var translationInfo in translationInfos)
        {
            await _transInvalid
                .InvalidateDeleteAsync(translationInfo);
        }

        if (translationInfos.Count == 0)
        {
            var basketAttributeInfo =
                await GetBasketAttributeInfos(entityInfo.Id);

            if (basketAttributeInfo is not null)
            {
                await _basketInvalid
                    .InvalidateDeleteAsync(basketAttributeInfo);
            }
        }
    }

    public async Task InvalidateUpdateAsync(
        AttributeDefinitionCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);

        var basketAttributeInfo =
            await GetBasketAttributeInfos(entityInfo.Id);

        if (basketAttributeInfo is not null)
        {
            await _basketInvalid
                    .InvalidateUpdateAsync(basketAttributeInfo);
        }
    }

    private async Task InvalidateAsync(
        AttributeDefinitionCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityLanguagePattern<AttributeDefinition>(entityInfo.Id));

        await InvalidatePatternAsync(entityInfo);
    }
    
    private async Task InvalidatePatternAsync(
        AttributeDefinitionCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
            LanguageSearchPattern<AttributeDefinition>());

        await _cache.RemoveByPatternAsync(CacheKeys.
           EntityLanguagePagedPattern<AttributeDefinition>());

        await _cache.RemoveByPatternAsync(AttributeDefinitionCache.
             ValueByCategoryPattern(entityInfo.CategoryId));
    }

    private async Task<BasketAttributeCacheInfo?> GetBasketAttributeInfos(
        long entityId)
    {
        return await _basketRepository
            .GetCacheInfoByAttributeDefinitionAsync(entityId);
    }

    private async Task<List<BaseTranslationCacheInfo>> GetTranslationInfos(
        long entityId)
    {
        return await _transRepository
            .GetCacheInfosByTranslatableAsync(entityId);
    }
}
