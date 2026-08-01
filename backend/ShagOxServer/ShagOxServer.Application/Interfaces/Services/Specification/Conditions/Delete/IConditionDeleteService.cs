using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Delete;
public interface IConditionDeleteService
{
    Task<Result<DeleteResponse>> DeleteAsync(
        int id);
}