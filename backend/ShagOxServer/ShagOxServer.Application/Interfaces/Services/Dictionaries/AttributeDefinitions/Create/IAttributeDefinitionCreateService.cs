using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Create;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Create;
public interface IAttributeDefinitionCreateService
    : ICreateService<
        CreateResponse,
        AttributeDefinitionCreateRequest
        >
{
}