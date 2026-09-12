using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Extensions;
public static class BasketItemQueryExtensions
{
    public static IQueryable<BasketItem> WithIncludes(
       this IQueryable<BasketItem> query)
    {
        return query
           .Include(x => x.Advertisement);
    }
}