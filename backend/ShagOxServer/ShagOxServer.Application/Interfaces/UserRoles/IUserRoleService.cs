using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Roles;
using ShagOxServer.Application.DTOs.Users;

namespace ShagOxServer.Application.Interfaces.UserRoles;
public interface IUserRoleService
{
    Task<Result<List<RoleDto>>> GetRolesByUserIdAsync(int userId);

    Task<Result<List<UserDto>>> GetUsersByRoleIdAsync(int roleId);
}
