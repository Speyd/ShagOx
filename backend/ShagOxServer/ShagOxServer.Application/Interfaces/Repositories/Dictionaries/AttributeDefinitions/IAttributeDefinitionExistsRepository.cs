using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions;
public interface IAttributeDefinitionExistsRepository 
    : IExistsRepository<AttributeDefinition>
{
    Task<bool> ExistsByCategoryAsync(int attributeId, int categoryId);

    Task<bool> ExistsByCategoryAsync(string attributeKey, int categoryId);
}