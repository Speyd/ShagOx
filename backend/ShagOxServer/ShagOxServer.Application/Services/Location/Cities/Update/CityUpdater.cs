using ShagOxServer.Application.DTOs.Location.Cities.Update;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Services.Location.Cities.Update;
public static class CityUpdater
{
    public static int ApplyUpdates(
        City city,
        CityUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.Code is not null)
        {
            city.Code = request.Code;
            countUpdated++;
        }

        if (request.RegionId.HasValue)
        {
            city.RegionId = request.RegionId.Value;
            countUpdated++;
        }

        return countUpdated;
    }
}