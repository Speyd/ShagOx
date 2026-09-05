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
        var code = request.Code ?? city.Code;

        if (regionId == city.RegionId &&
            code == city.Code)
        {
            return Result<(int, string)>.Fail("");
        }

        return Result<(int, string)>.Success((regionId, code));
    }
}