using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes;
public class AttributeDictionaryValueInvalidationService
    : ICacheInvalidationService<AttributeDictionaryValue, 
        AttributeDictionaryValueCacheInfo>
{

    //private readonly IAttributeDefinitionQueryRepository _attributeRepository;

    //private readonly AttributeDefinitionInvalidationService _attributeInvalid;

    private readonly ICacheService _cache;



    public AttributeDictionaryValueInvalidationService(
        ICacheService cache)
    {
        //_attributeRepository = attributeRepository;
        //_attributeInvalid = attributeInvalid;
        _cache = cache;
    }


    public async Task InvalidateDeleteAsync(
        AttributeDictionaryValueCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    public async Task InvalidateUpdateAsync(
        AttributeDictionaryValueCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);
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

    //private async Task<List<long>> GetAttributeDefinitionIds(
    //    long entityId)
    //{
    //    await _cache.RemoveByPatternAsync(CacheKeys
    //        .EntityLanguagePattern<AttributeDictionaryValue>(
    //            entityId));
    //}
}