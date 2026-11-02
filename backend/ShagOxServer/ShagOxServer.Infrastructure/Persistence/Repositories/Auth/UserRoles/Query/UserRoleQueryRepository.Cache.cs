using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Auth.Roles.Cache;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Auth.UserRoles;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.UserRoles.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.UserRoles.Query;
public partial class UserRoleQueryRepository
    : SearchRepository<UserRole, UserRoleSearchFilter>,
      IUserRoleQueryRepository
{
    public async Task<List<RoleCacheInfo>> GetRoleCacheInfoByUserAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Roles
            .WithStatusFilter(status)
            .Where(u => u.UserRoles
                .Any(ur => ur.UserId == userId))
            .SelectRoleCacheInfo()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<UserCacheInfo>> GetUserCacheInfoByRoleAsync(
        long roleId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Users
            .WithStatusFilter(status)
            .Where(u => u.UserRoles
                .Any(ur => ur.RoleId == roleId))
            .SelectUserCacheInfo()
            .ToListAsync(cancellationToken);
    }
}
