using ShagOxServer.Application.DTOs.Specification.Conditions.Update;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Update;
public interface IConditionUpdateService
{
    Task<Result<ConditionUpdateResponse>> UpdateAsync(
        int conditionId,
        ConditionUpdateRequest request);
}