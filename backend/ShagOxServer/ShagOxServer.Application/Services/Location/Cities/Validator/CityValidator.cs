using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Cities.Validator;
public class CityValidator
{
    private readonly IRepository<City> _cityRepository;
    private readonly ICityExistsRepository _cityExistsRepository;


    public CityValidator(
        IRepository<City> cityRepository,
        ICityExistsRepository cityExistsRepository)
    {
        _cityRepository = cityRepository;
        _cityExistsRepository = cityExistsRepository;
    }


    public async Task<Result<City>> GetByIdAsync(
        int cityId)
    {
        var city = await _cityRepository.GetByIdAsync(cityId);
        if (city is null)
            return Result<City>.NotFound("City");

        return Result<City>.Success(city);
    }

    public async Task<Result<bool>> ExistsByIdAsync(
       int cityId)
    {
        if (!await _cityExistsRepository.ExistsByIdAsync(cityId))
            return Result<bool>.NotFound("City");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsAsync(
       int cityId)
    {
        if (await _cityExistsRepository.ExistsByIdAsync(cityId))
            return Result<bool>.AlreadyExists("City");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsAsync(
       int regionId,
       string cityName)
    {
        if (!await _cityExistsRepository.ExistsAsync(regionId, cityName))
            return Result<bool>.NotFound("City");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsAsync(
       int regionId,
       string cityName)
    {
        if (await _cityExistsRepository.ExistsAsync(regionId, cityName))
            return Result<bool>.AlreadyExists("City");

        return Result<bool>.Success(true);
    }
}