using ShagOxServer.Application.DTOs.Location.Cities.Translations.Update;
using ShagOxServer.Application.DTOs.Location.Regions.Translations.Update;
using ShagOxServer.Domain.Entities.Location.Translations;

namespace ShagOxServer.Application.Services.Location.Cities.Translations.Update;
public static class CityTranslationUpdater
{
    public static int ApplyUpdates(
        CityTranslation city,
        CityTranslationUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.CityId.HasValue)
        {
            city.CityId = request.CityId.Value;
            countUpdated++;
        }

        if (request.Language is not null)
        {
            city.Language = request.Language;
            countUpdated++;
        }

        if (request.Name is not null)
        {
            city.Name = request.Name;
            countUpdated++;
        }

        return countUpdated;
    }
}
