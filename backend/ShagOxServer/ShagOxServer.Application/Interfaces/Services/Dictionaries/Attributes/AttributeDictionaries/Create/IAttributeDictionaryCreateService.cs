using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaries.Create;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaries.Create;
public interface IAttributeDictionaryCreateService
    : ICreateService<
        CreateResponse,
        AttributeDictionaryCreateRequest
        >
{
}