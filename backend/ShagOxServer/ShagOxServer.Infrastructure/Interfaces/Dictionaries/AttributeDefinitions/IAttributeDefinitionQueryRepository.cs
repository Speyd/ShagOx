using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Interfaces.Dictionaries.AttributeDefinitions;
public interface IAttributeDefinitionQueryRepository
{
    Task<AttributeDefinition?> GetByIdAsync(int id);

    Task<List<AttributeDefinition>> GetByIdsAsync(List<int> ids);

    Task<List<AttributeDefinition>> GetByCategoryAsync(int categoryId);

    Task<List<AttributeDefinition>> SearchByKey(
        string key,
        PaginationParams pagination);
}
