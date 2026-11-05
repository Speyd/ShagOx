
using ShagOxServer.Application.DTOs.Location.Cities.Query;
using ShagOxServer.Application.DTOs.Location.Regions.Query;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Services.Location.Cities.Mapping;
public static class CityMapper
{
    public static CityDto ToDto(
        City? city,
        RegionDto regionDto,
        string? lableCity)
    {
        if(city is null)
            return new CityDto(
                -1,
                city.EntityStatus,
                "Unknown city",
                regionDto,
                null
            );
        return new CityDto(
            city.Id,
            city.EntityStatus,
            city.Code,
            regionDto,
            lableCity
        );
    }
}
