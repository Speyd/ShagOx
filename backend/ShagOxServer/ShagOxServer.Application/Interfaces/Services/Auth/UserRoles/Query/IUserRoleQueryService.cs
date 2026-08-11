using ShagOxServer.Application.DTOs.Auth.Roles;
using ShagOxServer.Application.DTOs.Auth.UserRoles;
using ShagOxServer.Application.DTOs.Auth.Users;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.UserRoles.Query;
public interface IUserRoleQueryService
     : IQueryService<UserRoleDto>
{
    Task<Result<List<RoleDto>>> GetRolesByUserIdAsync(
        int userId,
		PaginationParams pagination);

    Task<Result<List<UserDto>>> GetUsersByRoleIdAsync(
        int roleId,
		PaginationParams pagination);
}
