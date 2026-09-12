using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.Core.Extensions;
public static class BasketQueryExtensions
{
    public static IQueryable<Basket> WithIncludes(
       this IQueryable<Basket> query)
    {
        return query
           .Include(x => x.BasketItems);
    }
}