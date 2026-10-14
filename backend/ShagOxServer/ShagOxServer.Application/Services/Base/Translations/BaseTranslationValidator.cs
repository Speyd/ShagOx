using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Application.Resources.EntityNames.Extensions;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Filters;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Base.Translations;
public abstract class BaseTranslationValidator<TEntity, TTranslation>
    : BaseValidator<TTranslation>
    where TEntity : BaseEntity
    where TTranslation : BaseTranslation<TEntity>

{
    private readonly ITranslationExistsRepository<TEntity, TTranslation> 
        _translationExistsRepository;


    public BaseTranslationValidator(
        IQueryRepository<TTranslation> entityRepository,
        ITranslationExistsRepository<TEntity, TTranslation> translationExistsRepository
        )
        : base(entityRepository, translationExistsRepository)
    {
        _translationExistsRepository = translationExistsRepository;
    }

    public async Task<Result<bool>> ExistsAsync(
        long objectId,
        string language)
    {
        if (!await _translationExistsRepository
                .ExistsAsync(objectId, language))
        {
            return Result<bool>.NotFound(
                EntityNameExtensions.GetLocalizedName<TEntity>());
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsAsync(
        long cityId,
        string language)
    {
        if (await _translationExistsRepository
            .ExistsAsync(cityId, language))
        {
            return Result<bool>.AlreadyExists(
                EntityNameExtensions.GetLocalizedName<TEntity>());
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByLanguageAsync(
        string language)
    {
        if (!await _translationExistsRepository
                .ExistsByLanguageAsync(language))
        {
            return Result<bool>.NotFound(
                EntityNameExtensions.GetLocalizedName<TEntity>());
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByLanguageAsync(
        string language)
    {
        if (await _translationExistsRepository
                .ExistsByLanguageAsync(language))
        {
            return Result<bool>.AlreadyExists(
                EntityNameExtensions.GetLocalizedName<TEntity>());
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByIdentificatorAsync(
        string identificator,
        string language)
    {
        if (!await _translationExistsRepository
                .ExistsByIdentificatorAsync(identificator, language))
        {
            return Result<bool>.NotFound(
                EntityNameExtensions.GetLocalizedName<TEntity>());
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByIdentificatorAsync(
        string identificator, 
        string language)
    {
        if (await _translationExistsRepository
                .ExistsByIdentificatorAsync(identificator, language))
        {
            return Result<bool>.AlreadyExists(
                EntityNameExtensions.GetLocalizedName<TEntity>());
        }

        return Result<bool>.Success(true);
    }
}
