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
            if (request.Quantity.Value < 0)
                basket.Quantity = 0;
            else if (request.Quantity.Value > basket.Advertisement.Stock)
                basket.Quantity = basket.Advertisement.Stock;
            else
                basket.Quantity = request.Quantity.Value;

            countUpdated++;
        }

        return countUpdated;
    }
}