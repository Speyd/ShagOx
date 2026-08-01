using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Roles.Delete;
public interface IRoleDeleteService
{
    Task<Result<DeleteResponse>> DeleteAsync(
        int id);
}