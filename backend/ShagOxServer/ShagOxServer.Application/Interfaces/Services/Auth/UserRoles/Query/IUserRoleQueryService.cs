using ShagOxServer.Application.DTOs.Auth.Roles.Cache;
using ShagOxServer.Application.DTOs.Auth.Roles.Query;
using ShagOxServer.Application.DTOs.Auth.UserRoles;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Cache;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.UserRoles;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.UserRoles.Query;
public interface IUserRoleQueryService
     : IQueryService<UserRoleDto, UserRole, UserRoleSearchFilter>
{
    Task<Result<PagedResult<RoleDto>>> GetRolesByUserAsync(
        long userId,
		PaginationParams pagination);

    Task<Result<List<UserCacheInfo>>> GetUserCacheInfoByRoleAsync(
       long roleId);

    Task<Result<PagedResult<UserDto>>> GetUsersByRoleAsync(
        long roleId,
		PaginationParams pagination);

    Task<Result<List<RoleCacheInfo>>> GetRolesCacheInfoByUserAsync(
       long userId);
}
