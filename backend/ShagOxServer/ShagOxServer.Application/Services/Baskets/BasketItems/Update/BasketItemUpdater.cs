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


        var stock = basket.AdvertisementVariant?.Stock ?? basket.Advertisement?.Stock;
        stock ??= 1;

        if (request.Quantity.HasValue)
        {
            if (request.Quantity.Value < 0)
                basket.Quantity = 0;
            else if (request.Quantity.Value > stock)
                basket.Quantity = stock.Value;
            else
                basket.Quantity = request.Quantity.Value;

            countUpdated++;
        }

        return countUpdated;
    }
}
