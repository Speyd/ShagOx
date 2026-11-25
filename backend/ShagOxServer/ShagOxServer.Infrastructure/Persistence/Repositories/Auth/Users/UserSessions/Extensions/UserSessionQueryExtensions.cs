using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserSessions.Extensions;
public static class UserSessionQueryExtensions
{
    public static IQueryable<UserSession> WithIncludes(
        this IQueryable<UserSession> query)
    {
        return query
            .AsSplitQuery();
    }
}
