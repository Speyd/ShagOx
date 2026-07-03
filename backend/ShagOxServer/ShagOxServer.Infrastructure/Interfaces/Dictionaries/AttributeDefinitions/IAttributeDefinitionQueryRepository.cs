using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.AttributeDefinitions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Interfaces.Dictionaries.AttributeDefinitions;
public interface IAttributeDefinitionQueryRepository
{
    Task<AttributeDefinition?> GetByIdAsync(int id);

    Task<List<AttributeDefinition>> GetByIdsAsync(List<int> ids);

    Task<List<AttributeDefinition>> GetByCategoryAsync(int categoryId);

    Task<List<AttributeDefinition>> Search(
        AttributeDefinitionSearchFilter filter,
        PaginationParams pagination);
}
