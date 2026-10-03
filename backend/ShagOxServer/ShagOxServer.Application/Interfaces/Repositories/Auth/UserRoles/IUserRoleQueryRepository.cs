using ShagOxServer.Application.DTOs.Auth.Roles.Cache;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.UserRoles;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles;
public interface IUserRoleQueryRepository
    : IQueryRepository<UserRole, UserRoleSearchFilter>
{
    Task<PagedResult<Role>> GetRolesByUserAsync(
        long userId,
        PaginationParams pagination);

    Task<List<RoleCacheInfo>> GetRoleCacheInfoByUserAsync(
        long userId);

    Task<PagedResult<User>> GetUsersByRoleAsync(
        long roleId,
        PaginationParams pagination);

    Task<List<UserCacheInfo>> GetUserCacheInfoByRoleAsync(
        long roleId);
}