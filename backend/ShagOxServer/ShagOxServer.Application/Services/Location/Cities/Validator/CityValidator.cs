using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Cities.Validator;
public class CityValidator
    : BaseValidator<City>
{
    private readonly ICityExistsRepository _cityExistsRepository;


    public CityValidator(
        IRepository<City> cityRepository,
        ICityExistsRepository cityExistsRepository
        ) : base(cityRepository, cityExistsRepository)
    {
        _cityExistsRepository = cityExistsRepository;
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