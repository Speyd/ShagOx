using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Infrastructure.Interfaces.Dictionaries.AttributeDefinitions;
public interface IAttributeDefinitionQueryRepository
{
    Task<AttributeDefinition?> GetByIdAsync(int id);

    Task<List<AttributeDefinition>> GetByIdsAsync(List<int> ids);

    Task<List<AttributeDefinition>> GetByCategoryAsync(int categoryId);
}
