using ShagOxServer.Application.DTOs.Location.Cities.Cache;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Services.Location.Cities.Mapping;
public static class CityCacheMapper
{
    public static CityCacheInfo ToInfo(
        City city,
        EntityStatus status = EntityStatus.Active)
    {
        return new CityCacheInfo(
            city.Id,
            city.RegionId,
            status);
    }
}
