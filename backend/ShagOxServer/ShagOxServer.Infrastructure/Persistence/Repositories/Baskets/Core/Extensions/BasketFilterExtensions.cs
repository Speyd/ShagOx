using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.Core;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.Core.Extensions;
public static class BasketFilterExtensions
{
    public static IQueryable<Basket> Filter(
        this IQueryable<Basket> query,
        BasketSearchFilter filter)
    {
        if (filter is null)
            return query;


        if (filter.UserId is not null)
        {
            query = query
                .Where(u =>
                    u.UserId == filter.UserId);
        }

        return query;
    }
}