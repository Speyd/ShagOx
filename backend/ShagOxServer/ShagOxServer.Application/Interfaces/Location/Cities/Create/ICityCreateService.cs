using ShagOxServer.Application.DTOs.Location.Cities.Create;
using ShagOxServer.SharedKernel.Results;

namespace ShagOxServer.Application.Interfaces.Location.Cities.Create;
public interface ICityCreateService
{
    Task<Result<CityCreateResponse>> CreateCityAsync(
       CityCreateRequest request);
}
