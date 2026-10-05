using ShagOxServer.Application.DTOs.Advertisements.Favorites.Cache;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Mapping;
public static class FavoriteCacheMapper
{
    public static FavoriteCacheInfo ToList(
        Favorite x)
    {
        return new FavoriteCacheInfo
        (
            x.Id,
            x.UserId
        );
    }
}