using ShagOxServer.Application.DTOs.Advertisements.Favorites.Update;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Update;
public static class FavoriteUpdater
{
    public static int ApplyUpdates(
        Favorite favorite,
        FavoriteUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.UserId is not null)
        {
            favorite.UserId = request.UserId.Value;
            countUpdated++;
        }

        if (request.AdvertisementId is not null)
        {
            favorite.AdvertisementId = request.AdvertisementId.Value;
            countUpdated++;
        }

        return countUpdated;
    }
}