using ShagOxServer.Application.DTOs.Advertisements.Core;
using ShagOxServer.Application.DTOs.Auth.Users;
using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Advertisements.Favorites;
public sealed record FavoriteDto
(
    int Id,
    UserDto User,
    AdvertisementDto Advertisement
) : BaseDto(Id);