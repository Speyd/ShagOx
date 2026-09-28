using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions;
public interface IAttributeDefinitionQueryRepository
    : IQueryRepository<AttributeDefinition, 
        AttributeDefinitionSearchFilter>
{
    Task<List<AttributeDefinition>> GetByIdsAsync(
        List<long> ids);

    Task<AttributeDefinition?> GetByKeyAsync(
        string key);

    Task<List<AttributeDefinition>> GetByKeysAsync(
        long categoryId,
        IEnumerable<string> keys);
}