using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Create;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Create;
public static class AttributeDictionaryValueTranslationCreater
{
    public static AttributeDictionaryValueTranslation Create(
      AttributeDictionaryValueTranslationCreateRequest request)
    {
        return new AttributeDictionaryValueTranslation
        {
            TranslatableId = request.TranslatableId,
            Language = request.Language,
            Name = request.Name
        };
    }
}