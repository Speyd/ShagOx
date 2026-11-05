using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Query;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;

namespace ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Mapping;
public static class AttributeDictionaryValueTranslationMapper
{
    public static AttributeDictionaryValueTranslationDto ToDto(
        AttributeDictionaryValueTranslation x)
    {
        return new AttributeDictionaryValueTranslationDto
        (
            x.Id,
            x.EntityStatus,
            x.Name
        );
    }
}
