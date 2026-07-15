using ShagOxServer.Application.DTOs.Advertisements.Favorites.Create;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Create;
public static class FavoriteCreater
{
    public static Favorite CreateFavorite(
        FavoriteCreateRequest request)
    {
        return new Favorite
        {
            UserId = request.UserId,
            AdvertisementId = request.AdvertisementId,
        };
    }
}