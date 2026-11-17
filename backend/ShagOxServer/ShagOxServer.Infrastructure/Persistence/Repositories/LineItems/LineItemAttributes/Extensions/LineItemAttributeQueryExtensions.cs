using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.LineItems.LineItemAttributes.Cache;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.LineItems;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.LineItems.LineItemAttributes.Extensions;
public static class LineItemAttributeQueryExtensions
{
    public static IQueryable<LineItemAttribute> WithIncludes(
       this IQueryable<LineItemAttribute> query)
    {
        return query
            .AsSplitQuery()
            .Include(x => x.AttributeDefinition)
                .ThenInclude(x => x.Category);
    }

    public static IQueryable<LineItemAttributeCacheInfo> SelectCacheInfo(
        this IQueryable<LineItemAttribute> query,
        EntityStatus status = EntityStatus.Active)
    {
        return query.Select(x => new LineItemAttributeCacheInfo(
            x.Id,
            x.AttributeDefinition.CategoryId,
            x.AttributeDefinitionId,
            status));
    }
}
