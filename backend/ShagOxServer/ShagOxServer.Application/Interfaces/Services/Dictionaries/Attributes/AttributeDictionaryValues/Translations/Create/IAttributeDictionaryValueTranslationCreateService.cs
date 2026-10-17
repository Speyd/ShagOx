using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Base.Create;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Create;
public interface IAttributeDictionaryValueTranslationCreateService
    : ICreateService<
        CreateResponse,
        AttributeDictionaryValueTranslationCreateRequest
        >
{
}