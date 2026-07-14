using ShagOxServer.Application.DTOs.Location.Cities.Update;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Cities.Update.Validator;
public class CityUpdateValidator
{
    public Result<(int regionId, string name)> HasChangesValidator(
        City city,
        CityUpdateRequest request)
    {
        var regionId = request.RegionId ?? city!.RegionId;
        var name = request.Name ?? city.Name;

        if (regionId == city.RegionId &&
            name == city.Name)
        {
            return Result<(int, string)>.Fail("");
        }

        return Result<(int, string)>.Success((regionId, name));
    }
}