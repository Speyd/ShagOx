using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Cache;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Extensions;
public static class AttributeDefinitionQueryExtensions
{
    public static IQueryable<AttributeDefinition> WithIncludes(
        this IQueryable<AttributeDefinition> query)
    {
        return query.Include(x => x.Category);
    }

    public static IQueryable<AttributeDefinitionCacheInfo> SelectCacheInfo(
       this IQueryable<AttributeDefinition> query)
    {
        return query.Select(x => new AttributeDefinitionCacheInfo(
            x.Id,
            x.CategoryId));
    }
}
