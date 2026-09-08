using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Translations.Create;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Translations.Create;
public static class AttributeDefinitionTranslationCreater
{
    public static AttributeDefinitionTranslation Create(
      AttributeDefinitionTranslationCreateRequest request)
    {
        return new AttributeDefinitionTranslation
        {
            TranslatableId = request.AttributeId,
            Language = request.Language,
            Name = request.Name
        };
    }
}