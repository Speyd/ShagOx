using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions;
public interface IAttributeDefinitionRepository
{
    Task<AttributeDefinition?> GetByIdAsync(int id);

    Task AddAsync(AttributeDefinition attribute);

    Task<bool> UpdateAsync(AttributeDefinition attribute);

    Task DeleteAsync(AttributeDefinition attribute);
}
