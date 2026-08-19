using ShagOxServer.Application.DTOs.Advertisements.Statuses.Translations.Update;
using ShagOxServer.Domain.Entities.Advertisements.Translations;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Update;
public static class StatusTranslationUpdater
{
    public static int ApplyUpdates(
        StatusTranslation status,
        StatusTranslationUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.StatusId is not null)
        {
            status.StatusId = request.StatusId.Value;
            countUpdated++;
        }

        if (request.Language is not null)
        {
            status.Language = request.Language;
            countUpdated++;
        }

        if (request.Name is not null)
        {
            status.Name = request.Name;
            countUpdated++;
        }

        if (request.Description is not null)
        {
            status.Description = request.Description;
            countUpdated++;
        }

        return countUpdated;
    }
}