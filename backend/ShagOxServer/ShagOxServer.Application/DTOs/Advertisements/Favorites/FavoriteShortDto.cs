using ShagOxServer.Application.DTOs.Users;

namespace ShagOxServer.Application.DTOs.Advertisements.Favorites;
public sealed record FavoriteShortDto
(
    int Id,
    UserShortDto User,
    AdvertisementShortDto Advertisement
);