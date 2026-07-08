using ShagOxServer.Application.DTOs.Specification.Conditions.Update;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Roles.Specification.Conditions.Update;
public interface IConditionUpdateService
{
    Task<Result<ConditionUpdateResponse>> UpdateConditionAsync(
        int conditionId,
        ConditionUpdateRequest request);
}
