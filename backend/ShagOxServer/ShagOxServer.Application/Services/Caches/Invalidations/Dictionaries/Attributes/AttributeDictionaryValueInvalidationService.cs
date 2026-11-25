using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes.Translation;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Dictionaries.Attributes;
using ShagOxServer.Domain.Cache.Dictionaries.Attributes;
using ShagOxServer.Domain.Cache.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes;
public class AttributeDictionaryValueInvalidationService
    : ICacheInvalidationService<AttributeDictionaryValue, 
        AttributeDictionaryValueCacheContext>
{
    private readonly IAttributeDictionaryValueTranslationQueryRepository _transRepository;
    private readonly AttributeDictionaryValueTranslationInvalidationService _transInvalid;

    private readonly IDataCacheService _cache;


    public AttributeDictionaryValueInvalidationService(
        IAttributeDictionaryValueTranslationQueryRepository transRepository,
        AttributeDictionaryValueTranslationInvalidationService transInvalid,
        IDataCacheService cache)
    {
        _transRepository = transRepository;
        _transInvalid = transInvalid;
        _cache = cache;
    }


    public async Task InvalidateCreateAsync(
       AttributeDictionaryValueCacheContext entityInfo)
    {
        await InvalidatePatternAsync(entityInfo);
    }

    public async Task InvalidateDeleteAsync(
        AttributeDictionaryValueCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);


        var transaltionInfos = await GetTranslationInfos(
            entityInfo.Id);

        foreach (var transaltionInfo in transaltionInfos)
        {
            await _transInvalid
                .InvalidateDeleteAsync(transaltionInfo);
        }
    }

    public async Task InvalidateUpdateAsync(
        AttributeDictionaryValueCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);


        var transaltionInfos = await GetTranslationInfos(
            entityInfo.Id);

        foreach(var transaltionInfo in transaltionInfos)
        {
            await _transInvalid
                .InvalidateUpdateAsync(transaltionInfo);
        }
    }

    private async Task InvalidateAsync(
        AttributeDictionaryValueCacheContext entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys
            .EntityLanguagePattern<AttributeDictionaryValue>(
                entityInfo.Id));

        await InvalidatePatternAsync(entityInfo);
    }

    private async Task InvalidatePatternAsync(
        AttributeDictionaryValueCacheContext entityInfo)
    {
        await _cache.RemoveByPatternAsync(AttributeDictionaryValueCache
            .ByDictionaryPattern(entityInfo.DictionaryId));

        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityLanguagePagedPattern<AttributeDictionaryValue>(
                entityInfo.Status));

        await _cache.RemoveByPatternAsync(CacheKeys.
             LanguageSearchPattern<AttributeDictionaryValue>(
                entityInfo.Status));
    }


    private async Task<List<TranslationCacheContext>> GetTranslationInfos(
        long entityId)
    {
        return await _transRepository
            .GetCacheInfosByTranslatableAsync(entityId);
    }
}
