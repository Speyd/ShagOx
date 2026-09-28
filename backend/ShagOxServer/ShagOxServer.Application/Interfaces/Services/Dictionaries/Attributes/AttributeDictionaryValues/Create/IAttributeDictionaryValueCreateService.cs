using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Create;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Create;
public interface IAttributeDictionaryValueCreateService
    : ICreateService<
        CreateResponse,
        AttributeDictionaryValueCreateRequest
        >
{
}