using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions.Translations;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Specification.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Conditions.Translations.Validator;
public class ConditionTranslationValidator
    : BaseTranslationValidator<ConditionTranslation>
{
    private readonly IConditionTranslationExistsRepository _conditionExistsRepository;


    public ConditionTranslationValidator(
        IRepository<ConditionTranslation> conditionRepository,
        IConditionTranslationExistsRepository conditionExistsRepository
    ) : base(conditionRepository, conditionExistsRepository)
    {
        _conditionExistsRepository = conditionExistsRepository;
    }

    
    public async Task<Result<bool>> ExistsByNameAsync(
        string name)
    {
        if (!await _conditionExistsRepository
            .ExistsByNameAsync(name))
        {
            return Result<bool>.NotFound(typeof(ConditionTranslation));
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByNameAsync(
        string name)
    {
        if (await _conditionExistsRepository
            .ExistsByNameAsync(name))
        {
            return Result<bool>.AlreadyExists(typeof(ConditionTranslation));
        }

        return Result<bool>.Success(true);
    }
}