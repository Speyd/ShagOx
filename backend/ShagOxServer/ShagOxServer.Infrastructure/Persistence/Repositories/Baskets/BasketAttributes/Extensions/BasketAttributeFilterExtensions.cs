using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketAttributes;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketAttributes.Extensions;
public static class BasketAttributeFilterExtensions
{
    public static IQueryable<BasketAttribute> Filter(
        this IQueryable<BasketAttribute> query,
        BasketAttributeSearchFilter filter)
    {
        if (filter is null)
            return query;


        if (filter.AttributeDefinitionId.HasValue)
        {
            query = query.Where(u =>
                u.AttributeDefinitionId ==
                filter.AttributeDefinitionId);
        }

        if (filter.CategoryId.HasValue)
        {
            query = query.Where(u => 
                u.AttributeDefinition.CategoryId == filter.CategoryId);
        }

        return query;
    }
}