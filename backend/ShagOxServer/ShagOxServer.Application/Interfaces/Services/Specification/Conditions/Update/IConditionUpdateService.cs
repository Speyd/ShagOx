using ShagOxServer.Application.DTOs.Specification.Conditions.Update;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.Application.DTOs.Common.Responses;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Update;
public interface IConditionUpdateService
{
    Task<Result<UpdateResponse>> UpdateAsync(
        int conditionId,
        ConditionUpdateRequest request);
}