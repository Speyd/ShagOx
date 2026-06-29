using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Infrastructure.Interfaces.Dictionaries;
public interface IAttributeDefinitionRepository
{
    Task<AttributeDefinition?> GetByIdAsync(int id);

    Task<List<AttributeDefinition>> GetByIdsAsync(List<int> ids);

    Task<List<AttributeDefinition>> GetByCategoryAsync(int categoryId);


    Task<bool> ExistsByIdAsync(int id);

    Task<bool> ExistsByCategoryAsync(int attributeId, int categoryId);


    Task AddAsync(AttributeDefinition attribute);

    Task<bool> UpdateAsync(AttributeDefinition attribute);

    Task DeleteAsync(AttributeDefinition attribute);
}
