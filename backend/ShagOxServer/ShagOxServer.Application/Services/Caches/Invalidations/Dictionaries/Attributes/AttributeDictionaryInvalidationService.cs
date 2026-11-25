using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Cache.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes;
public class AttributeDictionaryInvalidationService
    : ICacheInvalidationService<AttributeDictionary,
        AttributeDictionaryCacheContext>
{

    private readonly IAttributeDefinitionQueryRepository _attributeRepository;
    private readonly AttributeDefinitionInvalidationService _attributeInvalid;

    private readonly IAttributeDictionaryValueQueryRepository _valueRepository;
    private readonly AttributeDictionaryValueInvalidationService _valueInvalid;

    private readonly IDataCacheService _cache;



    public AttributeDictionaryInvalidationService(
        IAttributeDefinitionQueryRepository attributeRepository,
        AttributeDefinitionInvalidationService attributeInvalid,
        IAttributeDictionaryValueQueryRepository valueRepository,
        AttributeDictionaryValueInvalidationService valueInvalid,
        IDataCacheService cache)
    {
        _attributeRepository = attributeRepository;
        _attributeInvalid = attributeInvalid;
        _valueRepository = valueRepository;
        _valueInvalid = valueInvalid;
        _cache = cache;
    }


    public async Task InvalidateCreateAsync(
        AttributeDictionaryCacheContext entityInfo)
    {
        await InvalidatePatternAsync(entityInfo);
    }

    public async Task InvalidateDeleteAsync(
        AttributeDictionaryCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);

        var attributeIds =
            await GetAttributeDefinitionIds(entityInfo.Id);

        foreach (var attributeId in attributeIds)
        {
            await _attributeInvalid
                .InvalidateDeleteAsync(attributeId);
        }

        var valueInfos =
            await GetValueInfos(entityInfo.Id);

        foreach (var valueInfo in valueInfos)
        {
            await _valueInvalid
                .InvalidateDeleteAsync(valueInfo);
        }
    }

    public async Task InvalidateUpdateAsync(
        AttributeDictionaryCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    private async Task InvalidateAsync(
        AttributeDictionaryCacheContext entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityLanguagePattern<AttributeDictionary>(
                entityInfo.Id));

        await InvalidatePatternAsync(entityInfo);
    }

    private async Task InvalidatePatternAsync(
        AttributeDictionaryCacheContext entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityLanguagePagedPattern<AttributeDictionary>(
                entityInfo.Status));

        await _cache.RemoveByPatternAsync(CacheKeys.
             LanguageSearchPattern<AttributeDictionary>(
                entityInfo.Status));
    }

    private async Task<List<AttributeDefinitionCacheContext>> GetAttributeDefinitionIds(
        long entityId)
    {
        return await _attributeRepository
            .GetCacheInfosByAttributeDictionaryAsync(entityId);
    }

    private async Task<List<AttributeDictionaryValueCacheContext>> GetValueInfos(
        long entityId)
    {
        return await _valueRepository
            .GetCacheInfosByDictionaryAsync(entityId);
    }
}
