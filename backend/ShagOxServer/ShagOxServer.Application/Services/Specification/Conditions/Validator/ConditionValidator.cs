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

    public async Task<Result<bool>> ExistsByIdValidator(
      int id)
    {
        var condition = await _existsRepository.ExistsByIdAsync(id);
        if (condition)
            return Result<bool>.AlreadyExists("Condition");

        return Result<bool>.Success(false);
    }

    public async Task<Result<bool>> ExistsByNameValidator(
      string? name)
    {
        if(name is null)
            return Result<bool>.Success(false);

        var condition = await _existsRepository.ExistsByNameAsync(name);
        if (condition)
            return Result<bool>.AlreadyExists("Condition");

        return Result<bool>.Success(false);
    }
}