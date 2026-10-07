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
        long dictionaryId);

    Task<List<AttributeDefinition>> GetByIdsAsync(
        List<long> ids);

    Task<List<AttributeDefinition>> GetByKeysAsync(
        long categoryId,
        IEnumerable<string> keys);
}
