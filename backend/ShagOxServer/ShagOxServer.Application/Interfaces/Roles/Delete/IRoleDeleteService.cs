using ShagOxServer.Application.DTOs.Roles.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Roles.Delete;
public interface IRoleDeleteService
{
    Task<Result<RoleDeleteResponse>> DeleteRoleAsync(
        int id);
}
