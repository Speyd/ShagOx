using ShagOxServer.SharedKernel.Results;
using ShagOxServer.Application.DTOs.Roles.Update;

namespace ShagOxServer.Application.Interfaces.Roles.Update;
public interface IRoleUpdateService
{
    Task<Result<RoleUpdateResponse>> UpdateRoleAsync(
        int roleId,
        RoleUpdateRequest request);
}
