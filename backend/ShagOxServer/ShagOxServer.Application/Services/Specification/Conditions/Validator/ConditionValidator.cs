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

    public async Task<Result<Condition>> GetByIdAsync(
       int conditionId)
    {
        var condition = await _repository.GetByIdAsync(conditionId);
        if (condition is null)
            return Result<Condition>.NotFound("Condition");

        return Result<Condition>.Success(condition);
    }

    public async Task<Result<bool>> ExistsByIdAsync(
      int id)
    {
        if (!await _existsRepository.ExistsByIdAsync(id))
            return Result<bool>.AlreadyExists("Condition");

        return Result<bool>.Success(false);
    }

    public async Task<Result<bool>> NotExistsByIdAsync(
      int id)
    {
        if (await _existsRepository.ExistsByIdAsync(id))
            return Result<bool>.AlreadyExists("Condition");

        return Result<bool>.Success(false);
    }

    public async Task<Result<bool>> ExistsByNameAsync(
      string? name)
    {
        if(name is null)
            return Result<bool>.Success(false);

        if (!await _existsRepository.ExistsByNameAsync(name))
            return Result<bool>.AlreadyExists("Condition");

        return Result<bool>.Success(false);
    }

    public async Task<Result<bool>> NotExistsByNameAsync(
      string? name)
    {
        if (name is null)
            return Result<bool>.Success(false);

        if (!await _existsRepository.ExistsByNameAsync(name))
            return Result<bool>.AlreadyExists("Condition");

        return Result<bool>.Success(false);
    }
}