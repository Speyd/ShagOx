using ShagOxServer.Application.DTOs.Auth.Roles;
using ShagOxServer.Application.DTOs.Auth.UserRoles;
using ShagOxServer.Application.DTOs.Auth.Users.Core;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Filters.Auth.UserRoles;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.UserRoles.Query;
public interface IUserRoleQueryService
     : IQueryService<UserRoleDto, UserRoleSearchFilter>
{
    Task<Result<PagedResult<RoleDto>>> GetRolesByUserIdAsync(
        long userId,
		PaginationParams pagination);

    Task<Result<PagedResult<UserDto>>> GetUsersByRoleIdAsync(
        long roleId,
		PaginationParams pagination);
}
