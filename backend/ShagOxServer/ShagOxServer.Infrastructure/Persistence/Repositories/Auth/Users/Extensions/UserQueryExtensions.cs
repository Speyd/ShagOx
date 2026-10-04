using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Cache;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Extensions;
public static class UserQueryExtensions
{
    public static IQueryable<User> WithIncludes(
        this IQueryable<User> query)
    {
        return query
            .Include(x => x.Avatar)
            .Include(x => x.City)
                .ThenInclude(x => x!.Region)
            .Include(x => x.UserRoles)
                .ThenInclude(r => r.Role);
    }

    public static IQueryable<UserCacheInfo> SelectCacheInfo(
        this IQueryable<User> query)
    {
        return query.Select(x => new UserCacheInfo(
            x.Id,
            x.Email,
            x.Phone,
            x.UserName));
    }
}
