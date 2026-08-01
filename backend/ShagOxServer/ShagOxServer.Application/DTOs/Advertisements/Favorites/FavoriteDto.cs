using ShagOxServer.Application.DTOs.Auth.Users;

namespace ShagOxServer.Application.DTOs.Advertisements.Favorites;
public sealed record FavoriteDto
(
    int Id,
    UserDto User,
    AdvertisementDto Advertisement
);