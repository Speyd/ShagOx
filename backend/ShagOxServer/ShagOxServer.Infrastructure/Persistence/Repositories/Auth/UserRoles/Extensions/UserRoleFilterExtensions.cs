using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.UserRoles;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.UserRoles.Extensions;
public static class UserRoleFilterExtensions
{
    public static IQueryable<UserRole> Filter(
        this IQueryable<UserRole> query,
        UserRoleSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (filter.UserId.HasValue)
        {
            query = query.Where(x =>
                x.UserId == filter.UserId);
        }

        if (filter.RoleId.HasValue)
        {
            query = query.Where(x =>
                x.RoleId == filter.RoleId);
        }

        return query;
    }
}