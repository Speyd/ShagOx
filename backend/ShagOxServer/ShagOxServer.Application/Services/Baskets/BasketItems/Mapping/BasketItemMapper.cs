using ShagOxServer.Application.DTOs.Advertisements.Core.Query.LineItems;
using ShagOxServer.Application.DTOs.Baskets.BasketItems.Query;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Mapping;
public static class BasketItemMapper
{
    public static BasketItemDto ToDto(
        BasketItem item,
        AdvertisementLineItemDto advert)
    {
        return new BasketItemDto(
            item.Id,
            item.EntityStatus,
            item.ParentId,
            item.AdvertisementId,
            item.AdvertisementVariantId,
            item.Quantity,
            advert
        );
    }
}
