using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Update;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Update;
public interface IAttributeDictionaryValueUpdateService
    : IUpdateService<UpdateResponse,
        AttributeDictionaryValueUpdateRequest>
{
}