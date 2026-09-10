using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketAttributes.Extensions;
public static class BasketAttributeQueryExtensions
{
    public static IQueryable<BasketAttribute> WithIncludes(
       this IQueryable<BasketAttribute> query)
    {
        return query
           .Include(x => x.AttributeDefinition);
    }
}