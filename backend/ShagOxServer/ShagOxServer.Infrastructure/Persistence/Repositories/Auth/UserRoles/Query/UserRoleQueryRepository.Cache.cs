using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Auth;
using ShagOxServer.Domain.Cache.Auth.User;
using ShagOxServer.Domain.Entities.Auth;
using ShagOxServer.Domain.Filters.Auth.UserRoles;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.UserRoles.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.UserRoles.Query;
public partial class UserRoleQueryRepository
    : SearchRepository<UserRole, UserRoleSearchFilter>,
      IUserRoleQueryRepository
{
    public async Task<List<RoleCacheContext>> GetRoleCacheInfoByUserAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        return await _db.Roles
            .WithStatusFilter(status)
            .Where(u => u.UserRoles
                .Any(ur => ur.UserId == userId))
            .SelectRoleCacheInfo(status)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<UserCacheContext>> GetUserCacheInfoByRoleAsync(
        long roleId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        return await _db.Users
            .WithStatusFilter(status)
            .Where(u => u.UserRoles
                .Any(ur => ur.RoleId == roleId))
            .SelectUserCacheInfo(status)
            .ToListAsync(cancellationToken);
    }
}
