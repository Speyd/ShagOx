using ShagOxServer.Application.DTOs.Advertisements.Favorites;
using ShagOxServer.Application.Services.Advertisements.Mapping;
using ShagOxServer.Application.Services.Auth.Users.Mapping;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Mapping;
public static class FavoriteShortMapper
{
    public static FavoriteShortDto ToDto(
        Favorite x)
    {
        return new FavoriteShortDto
        (
            x.Id,
            UserShortMapper.ToDto(x.User),
            AdvertisementShortMapper.ToDto(x.Advertisement)
        );
    }
}