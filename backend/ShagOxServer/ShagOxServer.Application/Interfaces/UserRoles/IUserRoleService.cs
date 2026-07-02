using ShagOxServer.SharedKernel.Results;
using ShagOxServer.Application.DTOs.Roles;
using ShagOxServer.Application.DTOs.Users;

namespace ShagOxServer.Application.Interfaces.UserRoles;
public interface IUserRoleService
{
    Task<Result<List<RoleDto>>> GetRolesByUserIdAsync(
        int userId,
        int page = 1,
        int pageSize = 20);

    Task<Result<List<UserDto>>> GetUsersByRoleIdAsync(
        int roleId,
        int page = 1,
        int pageSize = 20);
}
