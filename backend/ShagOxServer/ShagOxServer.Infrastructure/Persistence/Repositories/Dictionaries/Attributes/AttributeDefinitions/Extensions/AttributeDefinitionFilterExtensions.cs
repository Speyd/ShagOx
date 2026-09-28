using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Extensions;
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
            query = query.Where(u => u.Key != null &&
                EF.Functions.ILike(u.Key, $"%{filter.Key}%"));
        }

        if (filter.CategoryId.HasValue)
        {
            query = query.Where(u => 
                u.CategoryId == filter.CategoryId.Value!);
        }

        return query;
    }
}