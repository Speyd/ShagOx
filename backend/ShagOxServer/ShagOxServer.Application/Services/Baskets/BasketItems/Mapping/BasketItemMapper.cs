using ShagOxServer.Application.DTOs.Baskets.BasketItems;
using ShagOxServer.Application.Services.Advertisements.Core.Mapping;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Mapping;
public static class BasketItemMapper
{
    public static BasketItemDto ToDto(
        BasketItem item)
    {
        return new BasketItemDto(
            item.Id,
            item.BasketId,
            AdvertisementShortMapper.ToDto(item.Advertisement),
            item.Quantity
        );
    }
}