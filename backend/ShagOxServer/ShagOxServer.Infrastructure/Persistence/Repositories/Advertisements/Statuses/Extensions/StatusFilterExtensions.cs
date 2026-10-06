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
        Console.WriteLine($"FILTER CODE: '{filter?.Code}'");

        if (!string.IsNullOrWhiteSpace(filter?.Code))
        {
            var pattern = $"%{filter.Code}%";

            Console.WriteLine($"FILTER PATTERN: '{pattern}'");

            query = query.Where(x =>
                EF.Functions.ILike(x.Code, pattern));
        }

        return query;
    }
}