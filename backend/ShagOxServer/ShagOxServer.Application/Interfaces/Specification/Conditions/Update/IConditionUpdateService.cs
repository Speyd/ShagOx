using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Specification.Conditions.Update;

namespace ShagOxServer.Application.Interfaces.Specification.Conditions.Update;
public interface IConditionUpdateService
{
    Task<Result<ConditionUpdateResponse>> UpdateConditionAsync(
        int conditionId,
        ConditionUpdateRequest request);
}
