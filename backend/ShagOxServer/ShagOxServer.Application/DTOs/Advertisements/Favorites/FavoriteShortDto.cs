using ShagOxServer.Application.DTOs.Auth.Users.Core;
using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Advertisements.Favorites;
public sealed record FavoriteShortDto
(
    long Id,
    UserShortDto User,
    AdvertisementShortDto Advertisement
) : BaseDto(Id);