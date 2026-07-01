using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Specification.Conditions.Update;
using ShagOxServer.Application.Interfaces.Specification.Conditions.Update;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Infrastructure.Interfaces.Specification;

namespace ShagOxServer.Application.Services.Specification.Conditions.Update;
public class ConditionUpdateService : IConditionUpdateService
{
    private readonly IConditionRepository _repository;

    public ConditionUpdateService(
        IConditionRepository conditionRepository)
    {
        _repository = conditionRepository;
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

        await _repository.UpdateAsync(condition);

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
