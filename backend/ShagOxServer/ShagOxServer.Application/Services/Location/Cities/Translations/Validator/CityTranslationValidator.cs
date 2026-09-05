using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions.Translations;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Cities.Translations.Validator;
public class CityTranslationValidator
{
    private readonly IRepository<CityTranslation> _cityRepository;
    private readonly IRegionTranslationExistsRepository _cityExistsRepository;


    public CityTranslationValidator(
        IRepository<CityTranslation> cityRepository,
        IRegionTranslationExistsRepository cityExistsRepository)
    {
        _cityRepository = cityRepository;
        _cityExistsRepository = cityExistsRepository;
    }


    public async Task<Result<CityTranslation>> GetByIdAsync(
        int cityId)
    {
        var region = await _cityRepository.GetByIdAsync(cityId);
        if (region is null)
            return Result<CityTranslation>.NotFound("City Translation");

        return Result<CityTranslation>.Success(region);
    }

    public async Task<Result<bool>> ExistsAsync(
        int cityId,
        string language)
    {
        if (!await _cityExistsRepository
            .ExistsAsync(cityId, language))
        {
            return Result<bool>.NotFound("City Translation");
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsAsync(
        int cityId,
        string language)
    {
        if (await _cityExistsRepository
            .ExistsAsync(cityId, language))
        {
            return Result<bool>.AlreadyExists("City Translation");
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByIdAsync(
        int cityId)
    {
        if (!await _cityExistsRepository
            .ExistsByIdAsync(cityId))
        {
            return Result<bool>.NotFound("City Translation");
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByIdAsync(
        int cityId)
    {
        if (await _cityExistsRepository
            .ExistsByIdAsync(cityId))
        {
            return Result<bool>.AlreadyExists("City Translation");
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByLanguageAsync(
        string language)
    {
        if (!await _cityExistsRepository
                .ExistsByLanguageAsync(language))
        {
            return Result<bool>.NotFound("City");
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByLanguageAsync(
        string language)
    {
        if (await _cityExistsRepository
                .ExistsByLanguageAsync(language))
        {
            return Result<bool>.AlreadyExists("City");
        }

        return Result<bool>.Success(true);
    }
}