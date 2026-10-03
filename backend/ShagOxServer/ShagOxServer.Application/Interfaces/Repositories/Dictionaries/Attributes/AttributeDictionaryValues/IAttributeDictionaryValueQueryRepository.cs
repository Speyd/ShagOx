using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues;
public interface IAttributeDictionaryValueQueryRepository
    : ITranslatableQueryRepository<AttributeDictionaryValue, 
        AttributeDictionaryValueSearchFilter>
{
    Task<PagedResult<AttributeDictionaryValue>> GetByDictionaryAsync(
        long dictionaryId,
        PaginationParams pagination);

    Task<List<AttributeDictionaryValueCacheInfo>> GetInfosByDictionaryAsync(
        long dictionaryId);

    Task<List<AttributeDictionaryValue>> GetByIdsAsync(
        IEnumerable<long> ids);

    Task<AttributeDictionaryValue?> GetAsync(
        string dictionaryCode,
        long valueId);

    Task<AttributeDictionaryValue?> GetAsync(
        long dictionaryId,
        long valueId);
}