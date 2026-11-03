using ShagOxServer.Application.DTOs.Specification.Conditions.Translations.Query;
using ShagOxServer.Domain.Entities.Specification.Translations;

namespace ShagOxServer.Application.Services.Specification.Conditions.Translations.Mapping;
public static class ConditionTranslationMapper
{
    public static ConditionTranslationDto ToDto(
        ConditionTranslation x)
    {
        return new ConditionTranslationDto
        (
            x.Id,
            x.EntityStatus,
            x.Name
        );
    }
}
