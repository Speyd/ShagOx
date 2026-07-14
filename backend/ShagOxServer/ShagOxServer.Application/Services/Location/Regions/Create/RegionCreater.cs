using ShagOxServer.Application.DTOs.Location.Regions.Create;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Services.Location.Regions.Create;
public static class RegionCreater
{
    public static Region CreateRegion(
        RegionCreateRequest request)
    {
        return new Region
        {
            Name = request.Name
        };
    }
}