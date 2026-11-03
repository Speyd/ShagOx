using ShagOxServer.Application.DTOs.Advertisements.Favorites.Cache;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Mapping;
public static class FavoriteCacheMapper
{
    public static FavoriteCacheInfo ToInfo(
        Favorite x,
        EntityStatus status = EntityStatus.Active)
    {
        return new FavoriteCacheInfo
        (
            x.Id,
            x.OwnerId,
            status
        );
    }
}
