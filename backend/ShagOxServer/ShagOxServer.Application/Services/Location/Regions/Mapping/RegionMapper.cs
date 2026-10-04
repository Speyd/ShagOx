using ShagOxServer.Application.DTOs.Location.Regions.Query;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Services.Location.Regions.Mapping;
public static class RegionMapper
{
    public static RegionDto ToDto(
        Region? region,
        string? lable)
    {
        if(region is null)
            return new RegionDto(
                -1,
                "Unknown code",
                null
            );

        return new RegionDto(
            region.Id,
            region.Code,
            lable
        );
    }
}