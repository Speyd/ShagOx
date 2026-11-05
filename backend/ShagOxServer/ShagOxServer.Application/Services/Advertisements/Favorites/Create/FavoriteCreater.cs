using ShagOxServer.Application.DTOs.Advertisements.Favorites.Create;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Create;
public static class FavoriteCreater
{
    public static Favorite Create(
        FavoriteCreateRequest request,
        long ownerId)
    {
        return new Favorite
        {
            OwnerId = ownerId,
            AdvertisementId = request.AdvertisementId,
        };
    }
}
