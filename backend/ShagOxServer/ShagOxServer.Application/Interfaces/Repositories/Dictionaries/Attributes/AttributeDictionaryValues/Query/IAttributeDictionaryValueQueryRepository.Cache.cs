using ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Query;
public partial interface IAttributeDictionaryValueQueryRepository
    : ISearchTranslatableRepository<AttributeDictionaryValue,
        AttributeDictionaryValueSearchFilter>
{
    Task<List<AttributeDictionaryValueCacheContext>> GetCacheInfosByDictionaryAsync(
        long dictionaryId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}
