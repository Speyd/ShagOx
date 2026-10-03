using ShagOxServer.Application.DTOs.Base.Cache;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes.Translation;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes;
public class AttributeDictionaryValueInvalidationService
    : ICacheInvalidationService<AttributeDictionaryValue, 
        AttributeDictionaryValueCacheInfo>
{
    private readonly IAttributeDictionaryValueTranslationQueryRepository _transRepository;
    private readonly AttributeDictionaryValueTranslationInvalidationService _transInvalid;

    private readonly ICacheService _cache;


    public AttributeDictionaryValueInvalidationService(
        IAttributeDictionaryValueTranslationQueryRepository transRepository,
        AttributeDictionaryValueTranslationInvalidationService transInvalid,
        ICacheService cache)
    {
        _transRepository = transRepository;
        _transInvalid = transInvalid;
        _cache = cache;
    }


    public async Task InvalidateDeleteAsync(
        AttributeDictionaryValueCacheInfo entityInfo)
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
        AttributeDictionaryValueCacheInfo entityInfo)
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
        AttributeDictionaryValueCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys
            .EntityLanguagePattern<AttributeDictionaryValue>(
                entityInfo.Id));

        await _cache.RemoveByPatternAsync(AttributeDictionaryValueCache
            .ByDictionaryPattern(entityInfo.DictionaryId));
    }

    private async Task<List<BaseTranslationCacheInfo>> GetTranslationInfos(
        long entityId)
    {
        return await _transRepository
            .GetCacheInfoByTranslatableAsync(entityId);
    }
}