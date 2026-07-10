using ShagOxServer.Application.DTOs.Specification.Conditions.Delete;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Conditions.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Conditions.Delete;
public class ConditionDeleteService : IConditionDeleteService
{
    private readonly IConditionRepository _repository;

    private readonly IUnitOfWork _unitOfWork;

    public ConditionDeleteService(
        IConditionRepository conditionRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = conditionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ConditionDeleteResponse>> DeleteConditionAsync(
        int id)
    {
        var condition = await _repository.GetByIdAsync(id);
        if (condition is null)
            return Result<ConditionDeleteResponse>.NotFound("Condition");

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _repository.Delete(condition);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }


        return Result<ConditionDeleteResponse>.Success(
           new ConditionDeleteResponse(
               condition.Id,
               DateTime.UtcNow
           )
       );
    }
}