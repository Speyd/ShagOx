using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Extensions;
public static class AttributeDefinitionQueryExtensions
{
    public static IQueryable<AttributeDefinition> WithIncludes(
        this IQueryable<AttributeDefinition> query,
        EntityStatus? status = null)
    {
        return query
            .AsSplitQuery()
            .Include(x => x.Category);
    }

    public static IQueryable<AttributeDefinitionCacheContext> SelectCacheInfo(
       this IQueryable<AttributeDefinition> query,
        EntityStatus status = EntityStatus.Active)
    {
        return query.Select(x => new AttributeDefinitionCacheContext(
            x.Id,
            x.CategoryId,
            status));
    }
}
