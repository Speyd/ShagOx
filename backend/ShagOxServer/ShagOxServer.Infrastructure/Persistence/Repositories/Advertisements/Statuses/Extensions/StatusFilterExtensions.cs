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

        if (!filter.Code.HasValue)
        {

            query = query.Where(x =>
                EF.Functions.ILike(x.Code.ToString(), $"%{filter.Code}%"));
        }

        return query;
    }
}
