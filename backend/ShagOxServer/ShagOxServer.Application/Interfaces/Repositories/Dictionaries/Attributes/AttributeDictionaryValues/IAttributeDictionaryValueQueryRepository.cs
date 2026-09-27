using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues;
public interface IAttributeDictionaryValueQueryRepository
    : IQueryRepository<AttributeDictionaryValue>
{
    Task<PagedResult<AttributeDictionaryValue>> GetByDictionaryAsync(
        long dictionaryId,
        PaginationParams pagination);

    Task<PagedResult<AttributeDictionaryValue>> Search(
        AttributeDictionaryValueSearchFilter filter,
        PaginationParams pagination);
}