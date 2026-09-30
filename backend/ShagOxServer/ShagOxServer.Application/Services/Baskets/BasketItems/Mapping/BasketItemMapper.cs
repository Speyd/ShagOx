using ShagOxServer.Application.DTOs.Advertisements.Core;
using ShagOxServer.Application.DTOs.Baskets.BasketItems;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Mapping;
public static class BasketItemMapper
{
    public static BasketItemDto ToDto(
        BasketItem item,
        AdvertisementDto advert)
    {
        return new BasketItemDto(
            item.Id,
            item.BasketId,
            item.Quantity,
            advert
        );
    }
}