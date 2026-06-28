using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Roles.Update;

namespace ShagOxServer.Application.Interfaces.Roles.Update;
public interface IRoleUpdateService
{
    Task<Result<RoleUpdateResponse>> UpdateRoleAsync(
        RoleUpdateRequest request);
}
