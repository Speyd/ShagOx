using ShagOxServer.Application.DTOs.Advertisements.Favorites;
using ShagOxServer.Application.Services.Advertisements.Mapping;
using ShagOxServer.Application.Services.Users.Mapping;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Mapping;
public static class FavoriteMapper
{
    public static FavoriteDto ToDto(Favorite x)
    {
        return new FavoriteDto
        (
            x.Id,
            UserMapper.ToDto(x.User),
            AdvertisementMapper.ToDto(x.Advertisement)
        );
    }
}
