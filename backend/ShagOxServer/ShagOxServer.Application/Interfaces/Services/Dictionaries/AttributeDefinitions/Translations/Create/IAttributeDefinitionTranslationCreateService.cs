using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Translations.Create;
public interface IAttributeDefinitionTranslationCreateService
    : ICreateService<
        CreateResponse,
        AttributeDefinitionTranslationCreateRequest
        >
{
}