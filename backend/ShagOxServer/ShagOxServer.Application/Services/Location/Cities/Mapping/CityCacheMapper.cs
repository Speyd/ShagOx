using ShagOxServer.Application.DTOs.Location.Cities;
using ShagOxServer.Application.DTOs.Location.Cities.Cache;
using ShagOxServer.Application.DTOs.Location.Regions;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Services.Location.Cities.Mapping;
public static class CityCacheMapper
{
    public static CityCacheInfo ToInfo(
        City city)
    {
        return new CityCacheInfo(
            city.Id,
            city.RegionId
            );
    }
}