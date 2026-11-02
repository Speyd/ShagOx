using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions;
public interface IAttributeDefinitionExistsRepository 
    : IExistsRepository<AttributeDefinition>
{
    Task<bool> ExistsByCategoryAsync(
        long attributeId,
        long categoryId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<bool> ExistsByCategoryAsync(
        string attributeKey,
        long categoryId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
