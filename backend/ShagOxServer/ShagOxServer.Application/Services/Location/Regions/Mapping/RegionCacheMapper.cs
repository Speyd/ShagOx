using ShagOxServer.Application.DTOs.Location.Regions.Cache;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Services.Location.Regions.Mapping;
public static class RegionCacheMapper
{
    public static RegionCacheInfo ToInfo(
        Region region,
        EntityStatus status = EntityStatus.Active)
    {
        return new RegionCacheInfo(
            region.Id,
            status
        );
    }
}
