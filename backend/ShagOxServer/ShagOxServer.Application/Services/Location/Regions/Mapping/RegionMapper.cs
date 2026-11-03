using ShagOxServer.Application.DTOs.Location.Regions.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Services.Location.Regions.Mapping;
public static class RegionMapper
{
    public static RegionDto ToDto(
        Region? region,
        string? label)
    {
        if(region is null)
            return new RegionDto(
                -1,
                EntityStatus.Active,
                "",
                null
            );

        return new RegionDto(
            region.Id,
            region.EntityStatus,
            region.Code,
            label
        );
    }
}
