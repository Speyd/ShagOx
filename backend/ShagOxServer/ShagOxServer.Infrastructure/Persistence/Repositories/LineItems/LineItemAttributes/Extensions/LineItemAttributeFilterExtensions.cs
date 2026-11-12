using ShagOxServer.Domain.Entities.LineItems;
using ShagOxServer.Domain.Filters.LineItems.LineItemAttributes;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.LineItems.LineItemAttributes.Extensions;
public static class LineItemAttributeFilterExtensions
{
    public static IQueryable<LineItemAttribute> Filter(
        this IQueryable<LineItemAttribute> query,
        LineItemAttributeSearchFilter filter)
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
