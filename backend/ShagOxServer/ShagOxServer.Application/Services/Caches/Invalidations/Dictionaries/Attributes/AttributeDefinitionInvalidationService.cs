using ShagOxServer.Application.DTOs.Advertisements.Core.Cache;
using ShagOxServer.Application.DTOs.Base.Cache;
using ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes.Translation;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Baskets;
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


    public async Task InvalidateCreateAsync(
        long entityId)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
             LanguageSearchPattern<AttributeDefinition>());
    }

    public async Task InvalidateDeleteAsync(
        long entityId)
    {
        await InvalidateAsync(entityId);

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
                    .InvalidateDeleteAsync(basketAttributeInfo);
            }
        }
    }

    public async Task InvalidateUpdateAsync(
        long entityId)
    {
        await InvalidateAsync(entityId);

        var basketAttributeInfo =
            await GetBasketAttributeInfos(entityId);

        if (basketAttributeInfo is not null)
        {
            await _basketInvalid
                    .InvalidateUpdateAsync(basketAttributeInfo);
        }
    }

    private async Task InvalidateAsync(
        long entityId)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityLanguagePattern<AttributeDefinition>(entityId));

        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityLanguagePagedPattern<AttributeDefinition>());

        await _cache.RemoveByPatternAsync(CacheKeys.
             LanguageSearchPattern<AttributeDefinition>());
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