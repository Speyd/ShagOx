using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Update;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Update;
public interface IAttributeDefinitionUpdateService
    : IUpdateService<UpdateResponse, AttributeDefinitionUpdateRequest>
{
}