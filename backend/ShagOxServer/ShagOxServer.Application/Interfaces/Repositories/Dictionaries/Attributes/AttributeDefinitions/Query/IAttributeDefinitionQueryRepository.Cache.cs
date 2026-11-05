using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions.Query;
public partial interface IAttributeDefinitionQueryRepository
    : ISearchTranslatableRepository<AttributeDefinition,
        AttributeDefinitionSearchFilter>
{
  
    Task<List<AttributeDefinitionCacheInfo>> GetCacheInfosByAttributeDictionaryAsync(
        long dictionaryId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}
