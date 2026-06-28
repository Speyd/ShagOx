using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Roles.Delete;

namespace ShagOxServer.Application.Interfaces.Roles.Delete;
public interface IRoleDeleteService
{
    Task<Result<RoleDeleteResponse>> DeleteRoleAsync(
        int id);
}
