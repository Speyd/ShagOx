using ShagOxServer.Application.DTOs.Location.Cities.Update;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Location.Cities.Update;
public interface ICityUpdateService
{
    Task<Result<CityUpdateResponse>> UpdateCityAsync(
       int cityId,
       CityUpdateRequest request);
}
