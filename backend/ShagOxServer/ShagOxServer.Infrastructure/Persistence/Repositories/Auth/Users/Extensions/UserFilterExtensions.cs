using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Users;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Extensions;
public static class UserFilterExtensions
{
    public static IQueryable<User> Filter(
        this IQueryable<User> query,
        UserSearchFilter filter)
    {
        if (filter is null) 
            return query;

        if (!string.IsNullOrWhiteSpace(filter.FullName))
        {
            query = query.Where(u => (u.Name + " " + u.Surname).Contains(filter.FullName));
        }

        if (!string.IsNullOrWhiteSpace(filter.Email))
        {
            query = query.Where(u => u.Email != null && u.Email.Contains(filter.Email));
        }

        if (!string.IsNullOrWhiteSpace(filter.Phone))
        {
            query = query.Where(u => u.Phone != null && u.Phone.Contains(filter.Phone));
        }

        return query;
    }

    public static IQueryable<User> Filter(
        this IQueryable<User> query,
        UserAdminSearchFilter filter)
    {
        if (filter is null)
            return query;


        if (!string.IsNullOrWhiteSpace(filter.FullName))
        {
            query = query.Where(u =>
                EF.Functions.ILike((u.Name + " " + u.Surname), $"%{filter.FullName}%"));
        }

        if (!string.IsNullOrWhiteSpace(filter.Email))
        {
            query = query.Where(u => u.Email != null &&
                EF.Functions.ILike(u.Email, $"%{filter.Email}%"));
        }

        if (!string.IsNullOrWhiteSpace(filter.Phone))
        {
            query = query.Where(u => u.Phone != null &&
                EF.Functions.ILike(u.Phone, $"%{filter.Phone}%"));
        }

        if (filter.CityId is not null)
        {
            query = query.Where(u => u.CityId == filter.CityId.Value!);
        }

        if (filter.RegisteredAfter is not null)
        {
            var dayStart = filter.RegisteredAfter.Value.Date;
            var dayEnd = dayStart.AddDays(1);

            return query.Where(x =>
                x.RegisteredAt >= dayStart && x.RegisteredAt < dayEnd);
        }

        if (filter.ActiveAfter is not null)
        {
            var dayStart = filter.ActiveAfter.Value.Date;
            var dayEnd = dayStart.AddDays(1);

            return query.Where(x =>
                x.LastSeenAt >= dayStart && x.LastSeenAt < dayEnd);
        }

        return query;
    }
}
