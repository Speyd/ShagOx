using ShagOxServer.SharedKernel.Results;
using ShagOxServer.Application.DTOs.Specification.Conditions.Create;

namespace ShagOxServer.Application.Interfaces.Specification.Conditions.Create;
public interface IConditionCreateService
{
    Task<Result<ConditionCreateResponse>> CreateConditionAsync(
       ConditionCreateRequest request);
}
