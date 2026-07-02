using ShagOxServer.SharedKernel.Results;
using ShagOxServer.Application.DTOs.Location.Cities.Delete;
using ShagOxServer.Application.Interfaces.Location.Cities.Delete;
using ShagOxServer.Infrastructure.Interfaces.Location.Cities;

namespace ShagOxServer.Application.Services.Location.Cities.Delete;
public class CityDeleteService : ICityDeleteService
{
    private readonly ICityRepository _repository;

    public CityDeleteService(
        ICityRepository cityRepository)
    {
        _repository = cityRepository;
    }

    public async Task<Result<CityDeleteResponse>> DeleteCityAsync(
        int id)
    {
        var city = await _repository.GetByIdAsync(id);
        if (city is null)
            return Result<CityDeleteResponse>.NotFound("City");

        await _repository.DeleteAsync(city);
        return Result<CityDeleteResponse>.Success(
           new CityDeleteResponse(
               city.Id,
               DateTime.UtcNow
           )
       );
    }
}
