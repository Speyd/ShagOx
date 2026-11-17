using ShagOxServer.Application.DTOs.Baskets.BasketItems.Update;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Update;
public static class BasketItemUpdater
{
    public static int ApplyUpdates(
        BasketItem basket,
        BasketItemUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.Quantity.HasValue)
        {
            var stock = basket.AdvertisementVariant?.Stock
                ?? basket.Advertisement.Stock;

            if (request.Quantity.Value < 0)
                basket.Quantity = 0;
            else if (request.Quantity.Value > stock)
                basket.Quantity = stock;
            else
                basket.Quantity = request.Quantity.Value;

            countUpdated++;
        }

        return countUpdated;
    }
}