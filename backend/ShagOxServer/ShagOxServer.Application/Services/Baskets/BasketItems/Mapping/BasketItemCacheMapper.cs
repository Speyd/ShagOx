using ShagOxServer.Application.DTOs.Baskets.BasketItems.Cache;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Mapping;
public static class BasketItemCacheMapper
{
    public static BasketItemCacheInfo ToInfo(
        BasketItem item,
        EntityStatus status = EntityStatus.Active)
    {
        return new BasketItemCacheInfo(
            item.Id,
            item.BasketId,
            item.AdvertisementId,
            item.AdvertisementVariantId,
            status
        );
    }
}
