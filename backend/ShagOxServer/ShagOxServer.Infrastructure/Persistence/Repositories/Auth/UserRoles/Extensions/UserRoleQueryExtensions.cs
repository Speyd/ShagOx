using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Auth;
using ShagOxServer.Domain.Cache.Auth.User;
using ShagOxServer.Domain.Entities.Auth;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.UserRoles.Extensions;
public static class UserRoleQueryExtensions
{
    public static IQueryable<Role> WithUserIncludes(
        this IQueryable<Role> query)
    {
        return query
            .AsSplitQuery()
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.User);
    }

    public static IQueryable<User> WithRoleIncludes(
        this IQueryable<User> query)
    {
        return query
            .AsSplitQuery()
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role);
    }

    public static IQueryable<RoleCacheContext> SelectRoleCacheInfo(
        this IQueryable<Role> query,
        EntityStatus status = EntityStatus.Active)
    {
        return query.Select(x => new RoleCacheContext(
            x.Id,
            x.Name,
            status));
    }

    public static IQueryable<UserCacheContext> SelectUserCacheInfo(
        this IQueryable<User> query,
        EntityStatus status = EntityStatus.Active)
    {
        return query.Select(x => new UserCacheContext(
            x.Id,
            x.Email,
            x.Phone,
            x.UserName,
            status));
    }
}
