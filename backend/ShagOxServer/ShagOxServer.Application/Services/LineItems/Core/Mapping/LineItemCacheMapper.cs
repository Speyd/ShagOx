using ShagOxServer.Application.DTOs.LineItems.Core.Cache;
using ShagOxServer.Domain.Entities.LineItems.Core;

namespace ShagOxServer.Application.Services.LineItems.Core.Mapping;
public static class LineItemCacheMapper
{
    public static LineItemCacheInfo ToInfo(
        LineItem item)
    {
        return new LineItemCacheInfo(
            item.Id,
            item.ParentId,
            item.AdvertisementId,
            item.AdvertisementVariantId,
            item.EntityStatus
        );
    }
}
