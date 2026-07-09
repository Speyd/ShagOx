using ShagOxServer.Application.DTOs.Roles;
using ShagOxServer.Application.DTOs.Users;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.UserRoles;
public interface IUserRoleService
{
    Task<Result<List<RoleDto>>> GetRolesByUserIdAsync(
        int userId,
		PaginationParams pagination);

    Task<Result<List<UserDto>>> GetUsersByRoleIdAsync(
        int roleId,
		PaginationParams pagination);
}
