using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.DTOs.Baskets.BasketItems;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Mapping;
public static class BasketItemMapper
{
    public static BasketItemDto ToDto(
        BasketItem item,
        AdvertisementVariantDto variant)
    {
        return new BasketItemDto(
            item.Id,
            item.BasketId,
            item.Quantity,
            variant
        );
    }
}