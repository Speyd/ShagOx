using ShagOxServer.Application.DTOs.Advertisements.Core.Query;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Advertisements.Favorites.Query;
public sealed record FavoriteDto
(
    long Id,
    UserDto User,
    AdvertisementShortDto Advertisement
) : BaseDto(Id);