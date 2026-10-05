using ShagOxServer.Application.DTOs.Auth.Roles.Cache;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.UserRoles;

namespace ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles.Query;
public partial interface IUserRoleQueryRepository
    : IQueryRepository<UserRole, UserRoleSearchFilter>
{
    Task<List<RoleCacheInfo>> GetRoleCacheInfoByUserAsync(
        long userId);

    Task<List<UserCacheInfo>> GetUserCacheInfoByRoleAsync(
        long roleId);
}