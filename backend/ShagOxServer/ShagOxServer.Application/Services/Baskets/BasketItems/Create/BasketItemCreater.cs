using ShagOxServer.Application.DTOs.Baskets.BasketItems.Create;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Create;
public static class BasketItemCreater
{
    public static BasketItem Create(
        BasketItemCreateRequest request,
        int stock)
    {
        return new BasketItem
        {
            BasketId = request.BasketId,
            AdvertisementVariantId = request.AdvertisementVariantId,
            Quantity = request.Quantity < 0? 0 : 
                request.Quantity > stock ? stock :
                request.Quantity,
        };
    }
}
