using ShagOxServer.Application.DTOs.LineItems.Create;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Create;
public static class BasketItemCreater
{
    public static BasketItem Create(
        LineItemCreateRequest request,
        int stock)
    {
        return new BasketItem
        {
            ParentId = request.BasketId,
            AdvertisementVariantId = request.AdvertisementVariantId,
            Quantity = request.Quantity < 0? 0 : 
                request.Quantity > stock ? stock :
                request.Quantity,
        };
    }
}
