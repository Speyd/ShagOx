using ShagOxServer.SharedKernel.Results;
using ShagOxServer.Application.DTOs.Location.Cities.Update;

namespace ShagOxServer.Application.Interfaces.Location.Cities.Update;
public interface ICityUpdateService
{
    Task<Result<CityUpdateResponse>> UpdateCityAsync(
       int cityId,
       CityUpdateRequest request);
}
