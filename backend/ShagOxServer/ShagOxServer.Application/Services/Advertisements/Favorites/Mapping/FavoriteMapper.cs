using ShagOxServer.Application.DTOs.Advertisements.Favorites.Query;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Application.Services.Advertisements.Core.Mapping;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Mapping;
public static class FavoriteMapper
{
    public static FavoriteDto ToDto(
        Favorite x,
        UserDto user)
    {
        return new FavoriteDto
        (
            x.Id,
            user,
            AdvertisementShortMapper.ToDto(x.Advertisement)
        );
    }
}