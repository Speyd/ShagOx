using ShagOxServer.Application.DTOs.Roles.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Roles.Delete;
public interface IRoleDeleteService
{
    Task<Result<RoleDeleteResponse>> DeleteAsync(
        int id);
}