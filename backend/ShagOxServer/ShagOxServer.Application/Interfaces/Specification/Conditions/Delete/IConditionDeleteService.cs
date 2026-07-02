using ShagOxServer.SharedKernel.Results;
using ShagOxServer.Application.DTOs.Specification.Conditions.Delete;

namespace ShagOxServer.Application.Interfaces.Specification.Conditions.Delete;
public interface IConditionDeleteService
{
    Task<Result<ConditionDeleteResponse>> DeleteConditionAsync(
        int id);
}
