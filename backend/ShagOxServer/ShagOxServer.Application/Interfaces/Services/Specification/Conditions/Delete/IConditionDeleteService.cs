using ShagOxServer.Application.DTOs.Specification.Conditions.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Delete;
public interface IConditionDeleteService
{
    Task<Result<ConditionDeleteResponse>> DeleteAsync(
        int id);
}