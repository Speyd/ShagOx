using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Cache;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketAttributes.Extensions;
public static class BasketAttributeQueryExtensions
{
    public static IQueryable<BasketAttribute> WithIncludes(
       this IQueryable<BasketAttribute> query)
    {
        return query
           .Include(x => x.AttributeDefinition)
                .ThenInclude(x => x.Category);
    }

    public static IQueryable<BasketAttributeCacheInfo> SelectCacheInfo(
        this IQueryable<BasketAttribute> query)
    {
        return query.Select(x => new BasketAttributeCacheInfo(
            x.Id,
            x.AttributeDefinition.CategoryId,
            x.AttributeDefinitionId));
    }
}