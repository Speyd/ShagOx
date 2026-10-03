using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes;
public class AttributeDictionaryInvalidationService
    : ICacheInvalidationService<AttributeDictionary, long>
{

    private readonly IAttributeDefinitionQueryRepository _attributeRepository;

    private readonly AttributeDefinitionInvalidationService _attributeInvalid;

    private readonly ICacheService _cache;



    public AttributeDictionaryInvalidationService(
        IAttributeDefinitionQueryRepository attributeRepository,
        AttributeDefinitionInvalidationService attributeInvalid,
        ICacheService cache)
    {
        _attributeRepository = attributeRepository;
        _attributeInvalid = attributeInvalid;
        _cache = cache;
    }


    public async Task InvalidateDeleteAsync(
        long entityId)
    {
        await InvalidateAsync(entityId);

        var attributeIds =
            await GetAttributeDefinitionIds(entityId);

        foreach (var attributeId in attributeIds)
        {
            await _attributeInvalid
                .InvalidateDeleteAsync(attributeId);
        }
    }

    public async Task InvalidateUpdateAsync(
        long entityId)
    {
        await InvalidateAsync(entityId);
    }

    private async Task InvalidateAsync(
        long entityId)
    {
        await _cache.RemoveAsync(CacheKeys.
            Entity<AttributeDictionary>(entityId));
    }

    private async Task<List<long>> GetAttributeDefinitionIds(
        long entityId)
    {
        return await _attributeRepository
            .GetIdsByAttributeDictionaryAsync(entityId);
    }
}