using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Conditions.Validator;
public class ConditionValidator
{
    private readonly IConditionRepository _repository;
    private readonly IConditionExistsRepository _existsRepository;


    public ConditionValidator(
        IConditionRepository conditionRepository,
        IConditionExistsRepository existsRepository)
    {
        _repository = conditionRepository;
        _existsRepository = existsRepository;
    }

    public async Task<Result<Condition>> GetConditionValidator(
       int conditionId)
    {
        var condition = await _repository.GetByIdAsync(conditionId);
        if (condition is null)
            return Result<Condition>.NotFound("Condition");

        return Result<Condition>.Success(condition);
    }

    public async Task<Result<bool>> ExistsByNameValidator(
      string name)
    {
        var condition = await _existsRepository.ExistsByNameAsync(name);
        if (!condition)
            return Result<bool>.NotFound("Condition");

        return Result<bool>.Success(true);
    }
}