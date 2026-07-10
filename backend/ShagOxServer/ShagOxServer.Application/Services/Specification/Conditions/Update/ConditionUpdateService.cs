using ShagOxServer.Application.DTOs.Specification.Conditions.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Conditions.Update;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Conditions.Update;
public class ConditionUpdateService : IConditionUpdateService
{
    private readonly IConditionRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ConditionUpdateService(
        IConditionRepository conditionRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = conditionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ConditionUpdateResponse>> UpdateConditionAsync(
        int conditionId, 
        ConditionUpdateRequest request)
    {
        var condition = await _repository.GetByIdAsync(conditionId);

        if (condition is null)
            return Result<ConditionUpdateResponse>.NotFound("Condition");

        var updatedCount = ApplyUpdates(condition, request);
        var result = new ConditionUpdateResponse(
                DateTime.UtcNow,
                updatedCount
            );

        if (updatedCount == 0)
            return Result<ConditionUpdateResponse>.Success(result);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _repository.Update(condition);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }


        return Result<ConditionUpdateResponse>.Success(result);
    }

    private static int ApplyUpdates(
        Condition condition,
        ConditionUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.Name is not null)
        {
            condition.Name = request.Name;
            countUpdated++;
        }


        return countUpdated;
    }
}
