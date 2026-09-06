using ShagOxServer.Application.DTOs.Location.Regions.Translations.Update;
using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Application.Services.Location.Regions.Translations.Update;
public static class RegionTranslationUpdater
{
    public static int ApplyUpdates(
        RegionTranslation region,
        RegionTranslationUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.RegionId.HasValue)
        {
            region.TranslatableId = request.RegionId.Value;
            countUpdated++;
        }

        if (request.Language is not null)
        {
            region.Language = request.Language;
            countUpdated++;
        }

        if (request.Name is not null)
        {
            region.Name = request.Name;
            countUpdated++;
        }

        return countUpdated;
    }
}