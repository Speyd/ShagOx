using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaries;
public interface IAttributeDictionaryQueryRepository
    : IQueryRepository<AttributeDictionary, 
        AttributeDictionarySearchFilter>
{
    Task<PagedResult<AttributeDefinition>> GetByDictionaryAsync(
        long attributeDictionaryId,
        PaginationParams pagination);
}