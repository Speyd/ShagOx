using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Extensions;
public static class AttributeDefinitionQueryExtensions
{
    public static IQueryable<AttributeDefinition> WithIncludes(
        this IQueryable<AttributeDefinition> query)
    {
        return query.Include(x => x.Category);
    }
}