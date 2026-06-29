using ShagOxServer.Application.DTOs.Location.Cities;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Services.Location.Cities.Mapping;
public static class CityMapper
{
    public static CityDto ToDto(City city)
    {
        return new CityDto(
            city.Name,
            city.RegionId,
            city.Region?.Name ?? "Unknown region name"
        );
    }
}
