using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Translations.Mapping;
public static class AttributeDefinitionTranslationMapper
{
    public static AttributeDefinitionTranslationDto ToDto(
        AttributeDefinitionTranslation x)
    {
        return new AttributeDefinitionTranslationDto
        (
            x.Id,
            x.Name
        );
    }
}