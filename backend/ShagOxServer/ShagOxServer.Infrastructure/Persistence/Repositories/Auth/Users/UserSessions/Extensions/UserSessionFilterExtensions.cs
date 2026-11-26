using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Filters.Auth.Users.UserSessions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserSessions.Extensions;
public static class UserSessionFilterExtensions
{
    public static IQueryable<UserSession> Filter(
        this IQueryable<UserSession> query,
        UserSessionSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (filter.UserId.HasValue)
        {
            query = query.Where(u =>
                u.OwnerId == filter.UserId.Value);
        }

        if (filter.UserDeviceId.HasValue)
        {
            query = query.Where(u =>
                u.UserDeviceId == filter.UserDeviceId.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.Browser))
        {
            query = query.Where(u =>
                EF.Functions.ILike(u.Browser, $"%{filter.Browser}%"));
        }

        if (!string.IsNullOrWhiteSpace(filter.BrowserVersion))
        {
            query = query.Where(u => u.BrowserVersion != null &&
                EF.Functions.ILike(u.BrowserVersion, $"%{filter.BrowserVersion}%"));
        }

        if (filter.RevocationReason.HasValue)
        {
            query = query.Where(u => u.RevocationReason != null &&
               u.RevocationReason == filter.RevocationReason.Value);
        }

        if (filter.IsActive.HasValue)
        {
            query = query.Where(u =>
                u.IsActive == filter.IsActive.Value);
        }

        return query;
    }
}
