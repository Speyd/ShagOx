using ShagOxServer.Application.DTOs.Location.Cities.Create;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Services.Location.Cities.Create;
public static class CityCreater
{
    public static City Create(
       CityCreateRequest request)
    {
        return new City
        {
            Code = request.Code,
            RegionId = request.RegionId,
        };
    }
}