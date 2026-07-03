using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.AttributeDefinitions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.AttributeDefinitions.Extensions;
public static class AttributeDefinitionFilterExtensions
{
    public static IQueryable<AttributeDefinition> Filter(
        this IQueryable<AttributeDefinition> query,
        AttributeDefinitionSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.Key))
        {
            query = query.Where(u => u.Key.Contains(filter.Key));
        }

        return query;
    }
}
