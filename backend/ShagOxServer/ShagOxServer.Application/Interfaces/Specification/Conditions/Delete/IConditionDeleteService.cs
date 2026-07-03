using ShagOxServer.Application.DTOs.Specification.Conditions.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Specification.Conditions.Delete;
public interface IConditionDeleteService
{
    Task<Result<ConditionDeleteResponse>> DeleteConditionAsync(
        int id);
}
