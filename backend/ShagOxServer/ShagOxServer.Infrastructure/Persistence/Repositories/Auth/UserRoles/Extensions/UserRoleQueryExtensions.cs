using Microsoft.EntityFrameworkCore;
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
}