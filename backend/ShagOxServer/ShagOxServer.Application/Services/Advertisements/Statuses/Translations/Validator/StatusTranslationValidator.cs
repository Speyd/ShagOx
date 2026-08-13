using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses.Translations;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Advertisements.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Validator;
public class StatusTranslationValidator
{
    private readonly IRepository<StatusTranslation> _statusRepository;
    private readonly IStatusTranslationExistsRepository _statusExistsRepository;


    public StatusTranslationValidator(
        IRepository<StatusTranslation> statusRepository,
        IStatusTranslationExistsRepository statusExistsRepository)
    {
        _statusRepository = statusRepository;
        _statusExistsRepository = statusExistsRepository;
    }


    public async Task<Result<StatusTranslation>> GetByIdAsync(
        int statusId)
    {
        var status = await _statusRepository.GetByIdAsync(statusId);
        if (status is null)
            return Result<StatusTranslation>.NotFound("Status Translation");

        return Result<StatusTranslation>.Success(status);
    }

    public async Task<Result<bool>> ExistsAsync(
        int statusId,
        string language)
    {
        if (!await _statusExistsRepository.ExistsAsync(statusId, language))
            return Result<bool>.NotFound("Status Translation");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsAsync(
        int statusId,
        string language)
    {
        if (await _statusExistsRepository.ExistsAsync(statusId, language))
            return Result<bool>.AlreadyExists("Status Translation");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByIdAsync(
        int statusId)
    {
        if (!await _statusExistsRepository.ExistsByIdAsync(statusId))
            return Result<bool>.NotFound("Status Translation");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByIdAsync(
        int statusId)
    {
        if (await _statusExistsRepository.ExistsByIdAsync(statusId))
            return Result<bool>.AlreadyExists("Status Translation");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByLanguageAsync(
        string language)
    {
        if (!await _statusExistsRepository
                .ExistsByLanguageAsync(language))
        {
            return Result<bool>.NotFound("Status");
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByLanguageAsync(
        string language)
    {
        if (await _statusExistsRepository
                .ExistsByLanguageAsync(language))
        {
            return Result<bool>.AlreadyExists("Status");
        }

        return Result<bool>.Success(true);
    }
}