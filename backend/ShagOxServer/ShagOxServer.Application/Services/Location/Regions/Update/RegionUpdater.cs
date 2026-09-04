using ShagOxServer.Application.DTOs.Location.Regions.Update;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Services.Location.Regions.Update;
public static class RegionUpdater
{
    public static int ApplyUpdates(
        Region region,
        RegionUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.Code is not null)
        {
            region.Code = request.Code;
            countUpdated++;
        }

        return countUpdated;
    }
}