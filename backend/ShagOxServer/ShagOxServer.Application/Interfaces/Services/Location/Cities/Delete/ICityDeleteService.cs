using ShagOxServer.Application.DTOs.Location.Cities.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Location.Cities.Delete;
public interface ICityDeleteService
{
    Task<Result<CityDeleteResponse>> DeleteAsync(
       int id);
}