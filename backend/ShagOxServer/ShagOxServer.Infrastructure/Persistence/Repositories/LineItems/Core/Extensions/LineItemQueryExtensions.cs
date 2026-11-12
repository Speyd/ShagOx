using ShagOxServer.Application.DTOs.LineItems.Cache;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.LineItems;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.LineItems.Core.Extensions;
public static class LineItemQueryExtensions
{
    public static IQueryable<LineItemCacheInfo> SelectCacheInfo(
        this IQueryable<LineItem> query,
        EntityStatus status = EntityStatus.Active)
    {
        return query.Select(x => new LineItemCacheInfo(
            x.Id,
            x.ParentId,
            x.AdvertisementId,
            x.AdvertisementVariantId,
            status));
    }
}
