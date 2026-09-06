using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions.Translations;
using ShagOxServer.Domain.Entities.Specification.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Conditions.Translations.Validator;
public class ConditionTranslationValidator
{
    private readonly IRepository<ConditionTranslation> _conditionRepository;
    private readonly IConditionTranslationExistsRepository _conditionExistsRepository;


    public ConditionTranslationValidator(
        IRepository<ConditionTranslation> conditionRepository,
        IConditionTranslationExistsRepository conditionExistsRepository)
    {
        _conditionRepository = conditionRepository;
        _conditionExistsRepository = conditionExistsRepository;
    }


    public async Task<Result<ConditionTranslation>> GetByIdAsync(
        int conditionId)
    {
        var condition = await _conditionRepository
            .GetByIdAsync(conditionId);

        if (condition is null)
            return Result<ConditionTranslation>.NotFound("Condition Translation");

        return Result<ConditionTranslation>.Success(condition);
    }

    public async Task<Result<bool>> ExistsAsync(
        int conditionId,
        string language)
    {
        if (!await _conditionExistsRepository
            .ExistsAsync(conditionId, language))
        {
            return Result<bool>.NotFound("Condition Translation");
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsAsync(
        int conditionId,
        string language)
    {
        if (await _conditionExistsRepository
            .ExistsAsync(conditionId, language))
        {
            return Result<bool>.AlreadyExists("Condition Translation");
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByIdAsync(
        int conditionId)
    {
        if (!await _conditionExistsRepository
            .ExistsByIdAsync(conditionId))
        {
            return Result<bool>.NotFound("Condition Translation");
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByIdAsync(
        int conditionId)
    {
        if (await _conditionExistsRepository
            .ExistsByIdAsync(conditionId))
        {
            return Result<bool>.AlreadyExists("Condition Translation");
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByLanguageAsync(
        string language)
    {
        if (!await _conditionExistsRepository
                .ExistsByLanguageAsync(language))
        {
            return Result<bool>.NotFound("Condition");
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByLanguageAsync(
        string language)
    {
        if (await _conditionExistsRepository
                .ExistsByLanguageAsync(language))
        {
            return Result<bool>.AlreadyExists("Condition");
        }

        return Result<bool>.Success(true);
    }
}