using ShagOxServer.Application.DTOs.Base.Cache;
using ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes.Translation;
using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes;
public class AttributeDefinitionInvalidationService
    : ICacheInvalidationService<AttributeDefinition, long>
{
    private readonly IBasketAttributeQueryRepository _basketRepository;

    private readonly BasketAttributeInvalidationService _basketInvalid;

    private readonly IAttributeDefinitionTranslationQueryRepository _transRepository;

    private readonly AttributeDefinitionTranslationInvalidationService _transInvalid;

    private readonly ICacheService _cache;



    public AttributeDefinitionInvalidationService(
        IBasketAttributeQueryRepository basketRepository,
        BasketAttributeInvalidationService basketInvalid,
        IAttributeDefinitionTranslationQueryRepository transRepository,
        AttributeDefinitionTranslationInvalidationService transInvalid,
        ICacheService cache)
    {
        _basketRepository = basketRepository;
        _basketInvalid = basketInvalid;
        _transRepository = transRepository;
        _transInvalid = transInvalid;
        _cache = cache;
    }


    public async Task InvalidateDeleteAsync(
        long entityId)
    {
        await _cache.RemoveAsync(CacheKeys.
            EntityLanguagePattern<AttributeDefinition>(entityId));


        var translationInfos =
            await GetTranslationInfos(entityId);

        foreach (var translationInfo in translationInfos)
        {
            await _transInvalid
                .InvalidateDeleteAsync(translationInfo);
        }

        if (translationInfos.Count == 0)
        {
            var basketAttributeInfo =
                await GetBasketAttributeInfos(entityId);

            if (basketAttributeInfo is not null)
            {
                await _basketInvalid
                    .InvalidateUpdateAsync(basketAttributeInfo);
            }
        }
    }

    public async Task InvalidateUpdateAsync(
        long entityId)
    {
        await _cache.RemoveAsync(CacheKeys.
             EntityLanguagePattern<AttributeDefinition>(entityId));


        var translationInfos =
            await GetTranslationInfos(entityId);

        foreach (var translationInfo in translationInfos)
        {
            await _transInvalid
                .InvalidateDeleteAsync(translationInfo);
        }


        if (translationInfos.Count == 0)
        {
            var basketAttributeInfo =
                await GetBasketAttributeInfos(entityId);

            if (basketAttributeInfo is not null)
            {
                await _basketInvalid
                    .InvalidateUpdateAsync(basketAttributeInfo);
            }
        }
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
            .GetCacheInfoByTranslatableAsync(entityId);
    }
}