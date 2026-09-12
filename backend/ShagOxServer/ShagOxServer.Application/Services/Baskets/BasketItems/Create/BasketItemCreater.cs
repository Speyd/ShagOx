using ShagOxServer.Application.DTOs.Baskets.BasketItems.Create;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Create;
public static class BasketItemCreater
{
    public static BasketItem Create(
       BasketItemCreateRequest request)
    {
        return new BasketItem
        {
            BasketId = request.BasketId,
            AdvertisementId = request.AdvertisementId,
            Quantity = request.Quantity < 0? 0 : request.Quantity,
        };
    }
}