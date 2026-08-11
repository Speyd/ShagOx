using ShagOxServer.Application.DTOs.Location.Regions;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Services.Location.Regions.Mapping;
public static class RegionMapper
{
    public static RegionDto ToDto(
        Region? region)
    {
        if(region is null)
            return new RegionDto(
                -1,
                "Unknown name"
            );

        return new RegionDto(
            region.Id,
            region.Name
        );
    }
}