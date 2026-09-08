using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Translations.Update;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Translations.Update;
public interface IAttributeDefinitionTranslationUpdateService
    : IUpdateService<UpdateResponse,
        AttributeDefinitionTranslationUpdateRequest>
{
}