using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Update;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Update;
public interface IAttributeDictionaryValueTranslationUpdateService
    : IUpdateService<UpdateResponse,
        AttributeDictionaryValueTranslationUpdateRequest>
{
}