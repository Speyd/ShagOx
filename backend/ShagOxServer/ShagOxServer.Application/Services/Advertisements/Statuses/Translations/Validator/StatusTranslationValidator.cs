using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses.Translations;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Advertisements.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Validator;
public class StatusTranslationValidator
    : BaseTranslationValidator<StatusTranslation>
{
    private readonly IStatusTranslationExistsRepository _statusExistsRepository;


    public StatusTranslationValidator(
        IRepository<StatusTranslation> statusRepository,
        IStatusTranslationExistsRepository statusExistsRepository
    ) : base(statusRepository, statusExistsRepository)
    {
        _statusExistsRepository = statusExistsRepository;
    }


   
    public async Task<Result<bool>> ExistsByNameAsync(
        string name)
    {
        if (!await _statusExistsRepository
            .ExistsByNameAsync(name))
        {
            return Result<bool>
                .NotFound(typeof(StatusTranslation));
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByNameAsync(
        string name)
    {
        if (await _statusExistsRepository
            .ExistsByNameAsync(name))
        {
            return Result<bool>
                .AlreadyExists(typeof(StatusTranslation));
        }

        return Result<bool>.Success(true);
    }
}