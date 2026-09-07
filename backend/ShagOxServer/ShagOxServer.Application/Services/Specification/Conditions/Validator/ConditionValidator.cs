using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Conditions.Validator;
public class ConditionValidator
    : BaseValidator<Condition>
{
    private readonly IConditionExistsRepository _conditionExistsRepository;


    public ConditionValidator(
        IRepository<Condition> conditionRepository,
        IConditionExistsRepository conditionExistsRepository
    ) : base(conditionRepository, conditionExistsRepository)
    {
        _conditionExistsRepository = conditionExistsRepository;
    }

    public async Task<Result<bool>> ExistsByNameAsync(
        string code)
    {

        if (!await _conditionExistsRepository
            .ExistsByCodeAsync(code))
        {
            return Result<bool>
                .NotFound(typeof(Condition));
        }

        return Result<bool>.Success(false);
    }

    public async Task<Result<bool>> NotExistsByCodeAsync(
        string code)
    {
        if (!await _conditionExistsRepository
            .ExistsByCodeAsync(code))
        {
            return Result<bool>
                .AlreadyExists(typeof(Condition));
        }

        return Result<bool>.Success(false);
    }
}