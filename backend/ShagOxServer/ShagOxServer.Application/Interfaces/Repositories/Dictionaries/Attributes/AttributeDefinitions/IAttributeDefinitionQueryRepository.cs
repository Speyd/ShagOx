using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions;
public interface IAttributeDefinitionQueryRepository
    : ISearchTranslatableRepository<AttributeDefinition, 
        AttributeDefinitionSearchFilter>
{
    Task<List<long>> GetIdsByAttributeDictionaryAsync(
        long dictionaryId,
        CancellationToken cancellationToken = default);

    Task<List<AttributeDefinitionCacheInfo>> GetCacheInfosByAttributeDictionaryAsync(
        long dictionaryId,
        CancellationToken cancellationToken = default);

    Task<List<AttributeDefinition>> GetByCategoryAsync(
        long categoryId,
        CancellationToken cancellationToken = default);

    Task<List<AttributeDefinition>> GetByIdsAsync(
        List<long> ids,
        CancellationToken cancellationToken = default);

    Task<List<AttributeDefinition>> GetByKeysAsync(
        long categoryId,
        IEnumerable<string> keys,
        CancellationToken cancellationToken = default);
}
