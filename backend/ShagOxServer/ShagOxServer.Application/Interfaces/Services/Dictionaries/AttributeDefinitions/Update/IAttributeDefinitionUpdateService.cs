using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Update;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Update;
public interface IAttributeDefinitionUpdateService
    : IUpdateService<UpdateResponse, AttributeDefinitionUpdateRequest>
{
}