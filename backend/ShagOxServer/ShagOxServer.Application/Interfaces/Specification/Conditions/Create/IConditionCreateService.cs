using ShagOxServer.Application.DTOs.Specification.Conditions.Create;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Specification.Conditions.Create;
public interface IConditionCreateService
{
    Task<Result<ConditionCreateResponse>> CreateConditionAsync(
       ConditionCreateRequest request);
}
