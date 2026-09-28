using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Translations.Create;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Create;
public static class AttributeDefinitionTranslationCreater
{
    public static AttributeDefinitionTranslation Create(
      AttributeDefinitionTranslationCreateRequest request)
    {
        return new AttributeDefinitionTranslation
        {
            TranslatableId = request.TranslatableId,
            Language = request.Language,
            Name = request.Name
        };
    }
}