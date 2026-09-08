using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Application.Services.Dictionaries.AttributeDefinitions.Translations.Mapping;
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