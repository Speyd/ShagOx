using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Query;
public partial interface IAttributeDictionaryValueQueryRepository
    : ITranslatableQueryRepository<AttributeDictionaryValue, 
        AttributeDictionaryValueSearchFilter>
{
    Task<PagedResult<AttributeDictionaryValue>> GetByDictionaryAsync(
        long dictionaryId,
        PaginationParams pagination);

    Task<AttributeDictionaryValue?> GetAsync(
        string dictionaryCode,
        long valueId);

    Task<AttributeDictionaryValue?> GetAsync(
        long dictionaryId,
        long valueId);
}