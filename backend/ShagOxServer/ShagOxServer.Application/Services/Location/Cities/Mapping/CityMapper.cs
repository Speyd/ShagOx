using ShagOxServer.Application.DTOs.Location.Cities;
using ShagOxServer.Application.Services.Location.Regions.Mapping;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Services.Location.Cities.Mapping;
public static class CityMapper
{
    public static CityDto ToDto(City city)
    {
        return new CityDto(
            city.Id,
            city.Name,
            RegionMapper.ToDto(city.Region)
        );
    }
}
