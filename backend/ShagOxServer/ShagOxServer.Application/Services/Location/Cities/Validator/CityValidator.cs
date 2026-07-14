using ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Cities.Validator;
public class CityValidator
{
    private readonly ICityRepository _cityRepository;
    private readonly ICityExistsRepository _cityExistsRepository;


    public CityValidator(
        ICityRepository cityRepository,
        ICityExistsRepository cityExistsRepository)
    {
        _cityRepository = cityRepository;
        _cityExistsRepository = cityExistsRepository;
    }


    public async Task<Result<City>> GetCityValidator(
        int cityId)
    {
        var city = await _cityRepository.GetByIdAsync(cityId);
        if (city is null)
            return Result<City>.NotFound("City");

        return Result<City>.Success(city);
    }

    public async Task<Result<bool>> ExistsCityValidator(
       int cityId)
    {
        var city = await _cityExistsRepository.ExistsAsync(cityId);
        if (!city)
            return Result<bool>.AlreadyExists("City");

        return Result<bool>.Success(city);
    }
}