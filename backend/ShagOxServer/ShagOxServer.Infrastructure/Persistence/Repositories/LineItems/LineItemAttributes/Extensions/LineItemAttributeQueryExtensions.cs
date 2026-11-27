using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.LineItems;
using ShagOxServer.Domain.Entities.LineItems;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.LineItems.LineItemAttributes.Extensions;
public static class LineItemAttributeQueryExtensions
{
    public static IQueryable<LineItemAttribute> WithIncludes(
       this IQueryable<LineItemAttribute> query,
        EntityStatus? status = null)
    {
        return query
            .AsSplitQuery()
            .Include(x => x.AttributeDefinition)
                .ThenInclude(x => x.Category);
    }

    public static IQueryable<LineItemAttributeCacheContext> SelectCacheInfo(
        this IQueryable<LineItemAttribute> query,
        EntityStatus status = EntityStatus.Active)
    {
        return query.Select(x => new LineItemAttributeCacheContext(
            x.Id,
            x.AttributeDefinition.CategoryId,
            x.AttributeDefinitionId,
            status));
    }
}
