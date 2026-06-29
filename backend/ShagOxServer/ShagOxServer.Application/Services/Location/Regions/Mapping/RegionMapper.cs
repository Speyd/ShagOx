using ShagOxServer.Application.DTOs.Location.Regions;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Services.Location.Regions.Mapping;
public static class RegionMapper
{
    public static RegionDto ToDto(Region region)
    {
        return new RegionDto(
            region.Name
        );
    }
}
