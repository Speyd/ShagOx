using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions;
public interface IAttributeDefinitionExistsRepository 
    : IExistsRepository<AttributeDefinition>
{
    Task<bool> ExistsByCategoryAsync(
        long attributeId,
        long categoryId);

    Task<bool> ExistsByCategoryAsync(
        string attributeKey,
        long categoryId);
}