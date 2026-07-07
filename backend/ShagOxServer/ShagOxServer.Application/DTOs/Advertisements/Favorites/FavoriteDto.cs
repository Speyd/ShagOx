using ShagOxServer.Application.DTOs.Users;

namespace ShagOxServer.Application.DTOs.Advertisements.Favorites;
public sealed record FavoriteDto
(
    UserShortDto User,
    AdvertisementShortDto Advertisement
);