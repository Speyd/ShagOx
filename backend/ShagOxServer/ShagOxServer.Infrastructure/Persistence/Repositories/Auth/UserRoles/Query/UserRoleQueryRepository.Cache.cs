using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Auth.Roles.Cache;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles.Query;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.UserRoles;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.UserRoles.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.UserRoles.Query;
public partial class UserRoleQueryRepository
    : SearchRepository<UserRole, UserRoleSearchFilter>,
      IUserRoleQueryRepository
{
    public async Task<List<RoleCacheInfo>> GetRoleCacheInfoByUserAsync(
        long userId)
    {
        return await _db.Roles
            .Where(u => u.UserRoles
                .Any(ur => ur.UserId == userId))
            .SelectRoleCacheInfo()
            .ToListAsync();
    }

    public async Task<List<UserCacheInfo>> GetUserCacheInfoByRoleAsync(
        long roleId)
    {
        return await _db.Users
            .Where(u => u.UserRoles
                .Any(ur => ur.RoleId == roleId))
            .SelectUserCacheInfo()
            .ToListAsync();
    }
}
