using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaries.Update;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaries.Update;
public interface IAttributeDictionaryUpdateService
    : IUpdateService<UpdateResponse, AttributeDictionaryUpdateRequest>
{
}