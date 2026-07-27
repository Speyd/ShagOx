using Microsoft.EntityFrameworkCore;
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
            query = query.Where(u => u.Key != null &&
                EF.Functions.ILike(u.Key, $"%{filter.Key}%"));
        }

        if (filter.CategoryId is not null)
        {
            query = query.Where(u => 
                u.CategoryId == filter.CategoryId.Value!);
        }

        return query;
    }
}