using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Base.Translations;
public abstract class BaseTranslationValidator<TObject>
    : BaseValidator<TObject>
    where TObject : class
{
    private readonly IExistsTranslationRepository<TObject> _translationExistsRepository;


    public BaseTranslationValidator(
        IRepository<TObject> objectRepository,
        IExistsTranslationRepository<TObject> translationExistsRepository
        )
        : base(objectRepository, translationExistsRepository)
    {
        _translationExistsRepository = translationExistsRepository;
    }

    public async Task<Result<bool>> ExistsAsync(
        int objectId,
        string language)
    {
        if (!await _translationExistsRepository
            .ExistsAsync(objectId, language))
        {
            return Result<bool>
                .NotFound(typeof(TObject).Name);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsAsync(
        int cityId,
        string language)
    {
        if (await _translationExistsRepository
            .ExistsAsync(cityId, language))
        {
            return Result<bool>
                .AlreadyExists(typeof(TObject).Name);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByLanguageAsync(
        string language)
    {
        if (!await _translationExistsRepository
                .ExistsByLanguageAsync(language))
        {
            return Result<bool>
                .NotFound(typeof(TObject).Name);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByLanguageAsync(
        string language)
    {
        if (await _translationExistsRepository
                .ExistsByLanguageAsync(language))
        {
            return Result<bool>
                .AlreadyExists(typeof(TObject).Name);
        }

        return Result<bool>.Success(true);
    }
}