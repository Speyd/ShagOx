using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Users;

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
}
