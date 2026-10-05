using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Auth.Roles.Cache;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Cache;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.UserRoles.Extensions;
public static class UserRoleQueryExtensions
{
    public static IQueryable<Role> WithUserIncludes(
        this IQueryable<Role> query)
    {
        return query
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.User);
    }

    public static IQueryable<User> WithRoleIncludes(
        this IQueryable<User> query)
    {
        return query
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role);
    }

    public static IQueryable<RoleCacheInfo> SelectRoleCacheInfo(
        this IQueryable<Role> query)
    {
        return query.Select(x => new RoleCacheInfo(
            x.Id,
            x.Name));
    }

    public static IQueryable<UserCacheInfo> SelectUserCacheInfo(
        this IQueryable<User> query)
    {
        return query.Select(x => new UserCacheInfo(
            x.Id,
            x.Email,
            x.Phone,
            x.UserName));
    }
}