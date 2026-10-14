using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Query;
public partial interface IAttributeDictionaryValueQueryRepository
    : ISearchTranslatableRepository<AttributeDictionaryValue,
        AttributeDictionaryValueSearchFilter>
{
    Task<List<AttributeDictionaryValueCacheInfo>> GetCacheInfosByDictionaryAsync(
        long dictionaryId,
        CancellationToken cancellationToken = default);

    Task<List<AttributeDictionaryValue>> GetByIdsAsync(
        IEnumerable<long> ids,
        CancellationToken cancellationToken = default);
}
