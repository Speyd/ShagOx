using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaries;
public interface IAttributeDictionaryQueryRepository
    : IQueryRepository<AttributeDictionary>
{
    Task<PagedResult<AttributeDefinition>> GetDefinitionsAsync(
        long attributeDictionaryId,
        PaginationParams pagination);

    Task<PagedResult<AttributeDictionary>> Search(
        AttributeDictionarySearchFilter filter,
        PaginationParams pagination);
}