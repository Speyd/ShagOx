using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Delete;
public interface IAttributeDefinitionDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<AttributeDefinition, DeleteResponse>
{
}
