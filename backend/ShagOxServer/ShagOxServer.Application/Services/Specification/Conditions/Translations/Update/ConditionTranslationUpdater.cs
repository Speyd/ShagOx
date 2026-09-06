using ShagOxServer.Application.DTOs.Specification.Conditions.Translations.Update;
using ShagOxServer.Domain.Entities.Specification.Translations;

namespace ShagOxServer.Application.Services.Specification.Conditions.Translations.Update;
public static class ConditionTranslationUpdater
{
    public static int ApplyUpdates(
        ConditionTranslation condition,
        ConditionTranslationUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.ConditionId.HasValue)
        {
            condition.TranslatableId = request.ConditionId.Value;
            countUpdated++;
        }

        if (request.Language is not null)
        {
            condition.Language = request.Language;
            countUpdated++;
        }

        if (request.Name is not null)
        {
            condition.Name = request.Name;
            countUpdated++;
        }

        return countUpdated;
    }
}
