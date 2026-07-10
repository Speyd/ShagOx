using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions;
public interface IAttributeDefinitionRepository
{
    Task<AttributeDefinition?> GetByIdAsync(int id);

    void Add(AttributeDefinition attribute);

    bool Update(AttributeDefinition attribute);

    void Delete(AttributeDefinition attribute);
}