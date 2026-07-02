using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Specification.Conditions.Delete;
using ShagOxServer.Application.Interfaces.Specification.Conditions.Delete;
using ShagOxServer.Infrastructure.Interfaces.Specification.Conditions;

namespace ShagOxServer.Application.Services.Specification.Conditions.Delete;
public class ConditionDeleteService : IConditionDeleteService
{
    private readonly IConditionRepository _repository;

    public ConditionDeleteService(
        IConditionRepository conditionRepository)
    {
        _repository = conditionRepository;
    }

    public async Task<Result<ConditionDeleteResponse>> DeleteConditionAsync(
        int id)
    {
        var condition = await _repository.GetByIdAsync(id);
        if (condition is null)
            return Result<ConditionDeleteResponse>.NotFound("Condition");

        await _repository.DeleteAsync(condition);
        return Result<ConditionDeleteResponse>.Success(
           new ConditionDeleteResponse(
               condition.Id,
               DateTime.UtcNow
           )
       );
    }
}
