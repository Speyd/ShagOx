using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses.Extensions;
public static class StatusFilterExtensions
{
    public static IQueryable<Status> Filter(
        this IQueryable<Status> query,
        StatusSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.Code))
        {
            query = query.Where(u =>
                EF.Functions.ILike(u.Code, $"%{filter.Code}%"));
        }

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            query = query.Where(u =>
                 EF.Functions.ILike(u.Name, $"%{filter.Name}%"));
        }

        if (!string.IsNullOrWhiteSpace(filter.Description))
        {
            query = query.Where(u =>
                 EF.Functions.ILike(u.Description, $"%{filter.Description}%"));
        }

        return query;
    }
}