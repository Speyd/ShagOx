using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Extensions;
public static class UserQueryExtensions
{
    public static IQueryable<User> WithIncludes(this IQueryable<User> query)
    {
        return query
            .Include(x => x.Avatar)
            .Include(x => x.City)
            .Include(x => x.UserRoles)
                .ThenInclude(r => r.Role);
    }
}