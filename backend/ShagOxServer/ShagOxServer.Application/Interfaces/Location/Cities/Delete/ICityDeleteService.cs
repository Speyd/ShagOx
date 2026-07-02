using ShagOxServer.SharedKernel.Results;
using ShagOxServer.Application.DTOs.Location.Cities.Delete;

namespace ShagOxServer.Application.Interfaces.Location.Cities.Delete;
public interface ICityDeleteService
{
    Task<Result<CityDeleteResponse>> DeleteCityAsync(
       int id);
}