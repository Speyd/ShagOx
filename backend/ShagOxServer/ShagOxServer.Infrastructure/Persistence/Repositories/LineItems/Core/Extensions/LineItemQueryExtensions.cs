using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.LineItems;
using ShagOxServer.Domain.Entities.LineItems.Core;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.LineItems.Core.Extensions;
public static class LineItemQueryExtensions
{
    public static IQueryable<LineItemCacheContext> SelectCacheInfo(
        this IQueryable<LineItem> query,
        EntityStatus status = EntityStatus.Active)
    {
        return query.Select(x => new LineItemCacheContext(
            x.Id,
            x.ParentId,
            x.AdvertisementId,
            x.AdvertisementVariantId,
            status));
    }
}
