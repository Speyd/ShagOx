using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Create;
public interface IAttributeDefinitionTranslationCreateService
    : ICreateService<
        CreateResponse,
        AttributeDefinitionTranslationCreateRequest
        >
{
}