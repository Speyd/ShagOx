using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities.Translations;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Location.Cities.Translations.Validator;
public class CityTranslationValidator
    : BaseTranslationValidator<CityTranslation>
{
    private readonly ICityTranslationExistsRepository _cityTranslationExistsRepository;


    public CityTranslationValidator(
        IRepository<CityTranslation> cityRepository,
        ICityTranslationExistsRepository cityTranslationExistsRepository
    ) : base(cityRepository, cityTranslationExistsRepository)

    {
        _cityTranslationExistsRepository = cityTranslationExistsRepository;
    }


    public async Task<Result<bool>> ExistsByNameAsync(
        string name)
    {
        if (!await _cityTranslationExistsRepository
                .ExistsByNameAsync(name))
        {
            return Result<bool>
                .NotFound(typeof(CityTranslation));
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByNameAsync(
        string name)
    {
        if (await _cityTranslationExistsRepository
                .ExistsByNameAsync(name))
        {
            return Result<bool>
                .AlreadyExists(typeof(CityTranslation));
        }

        return Result<bool>.Success(true);
    }
}