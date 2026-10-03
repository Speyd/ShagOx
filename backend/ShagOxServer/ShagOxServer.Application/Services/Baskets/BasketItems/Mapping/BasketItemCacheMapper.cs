using ShagOxServer.Application.DTOs.Baskets.BasketItems.Cache;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Mapping;
public static class BasketItemCacheMapper
{
    public static BasketItemCacheInfo ToInfo(
        BasketItem item)
    {
        return new BasketItemCacheInfo(
            item.Id,
            item.BasketId,
            item.AdvertisementVariantId
        );
    }
}