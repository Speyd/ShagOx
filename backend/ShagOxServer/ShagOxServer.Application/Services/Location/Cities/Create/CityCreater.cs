using ShagOxServer.Application.DTOs.Location.Cities.Create;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Services.Location.Cities.Create;
public static class CityCreater
{
    public static City CreateCity(
       CityCreateRequest request)
    {
        return new City
        {
            Name = request.Name,
            RegionId = request.RegionId,
        };
    }
}