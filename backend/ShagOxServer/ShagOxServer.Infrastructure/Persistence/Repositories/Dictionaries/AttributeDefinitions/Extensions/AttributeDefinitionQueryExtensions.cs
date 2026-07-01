using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.AttributeDefinitions.Extensions;
public static class AttributeDefinitionQueryExtensions
{
    public static IQueryable<AttributeDefinition> WithIncludes(
        this IQueryable<AttributeDefinition> query)
    {
        return query.Include(x => x.Category);
    }
}
