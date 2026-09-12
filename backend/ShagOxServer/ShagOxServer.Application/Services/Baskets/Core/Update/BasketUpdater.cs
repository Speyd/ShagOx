using ShagOxServer.Application.DTOs.Baskets.Core.Update;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Baskets.Core.Update;
public static class BasketUpdater
{
    public static int ApplyUpdates(
        Basket basket,
        BasketUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.UserId.HasValue)
        {
            basket.UserId = request.UserId.Value;

            countUpdated++;
        }

        return countUpdated;
    }
}