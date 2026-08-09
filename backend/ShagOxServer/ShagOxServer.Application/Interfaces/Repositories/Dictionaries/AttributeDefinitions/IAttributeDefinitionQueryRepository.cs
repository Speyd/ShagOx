using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.AttributeDefinitions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions;
public interface IAttributeDefinitionQueryRepository
    : IQueryRepository<AttributeDefinition>
{
    Task<List<AttributeDefinition>> GetByIdsAsync(List<int> ids);

    Task<PagedResult<AttributeDefinition>> Search(
        AttributeDefinitionSearchFilter filter,
        PaginationParams pagination);
}