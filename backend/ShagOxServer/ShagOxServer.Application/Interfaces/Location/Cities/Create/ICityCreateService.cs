using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Location.Cities.Create;

namespace ShagOxServer.Application.Interfaces.Location.Cities.Create;
public interface ICityCreateService
{
    Task<Result<CityCreateResponse>> CreateCityAsync(
       CityCreateRequest request);
}
