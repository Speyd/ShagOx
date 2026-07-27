using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Roles;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Roles.Extensions;
public static class RoleFilterExtensions
{
    public static IQueryable<Role> Filter(
        this IQueryable<Role> query,
        RoleSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            query = query.Where(u =>
                EF.Functions.ILike(u.Name, $"%{filter.Name}%"));
        }

        return query;
    }
}
