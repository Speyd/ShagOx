using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions;
public interface IAttributeDefinitionQueryRepository
    : IQueryRepository<AttributeDefinition>
{
    Task<List<AttributeDefinition>> GetByIdsAsync(
        List<long> ids);

    Task<PagedResult<AttributeDefinition>> Search(
        AttributeDefinitionSearchFilter filter,
        PaginationParams pagination);
}