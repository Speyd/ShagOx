using ShagOxServer.Application.DTOs.Advertisements.Core.Query.Core;
using ShagOxServer.Application.DTOs.Baskets.BasketItems.Query;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Mapping;
public static class BasketItemMapper
{
    public static BasketItemDto ToDto(
        BasketItem item,
        AdvertisementShortDto advert)
    {
        return new BasketItemDto(
            item.Id,
            item.EntityStatus,
            item.BasketId,
            item.AdvertisementId,
            item.AdvertisementVariantId,
            item.Quantity,
            advert
        );
    }
}
