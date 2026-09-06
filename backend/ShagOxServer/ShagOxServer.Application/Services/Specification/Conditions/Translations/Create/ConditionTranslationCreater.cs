using ShagOxServer.Application.DTOs.Specification.Conditions.Translations.Create;
using ShagOxServer.Domain.Entities.Specification.Translations;

namespace ShagOxServer.Application.Services.Specification.Conditions.Translations.Create;
public static class ConditionTranslationCreater
{
    public static ConditionTranslation Create(
        ConditionTranslationCreateRequest request)
    {
        return new ConditionTranslation
        {
            TranslatableId = request.ConditionId,
            Language = request.Language,
            Name = request.Name
        };
    }
}