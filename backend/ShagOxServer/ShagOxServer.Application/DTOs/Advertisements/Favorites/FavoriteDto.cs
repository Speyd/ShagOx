using ShagOxServer.Application.DTOs.Advertisements.Core;
using ShagOxServer.Application.DTOs.Auth.Users.Core;
using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Advertisements.Favorites;
public sealed record FavoriteDto
(
    long Id,
    UserDto User,
    AdvertisementDto Advertisement
) : BaseDto(Id);