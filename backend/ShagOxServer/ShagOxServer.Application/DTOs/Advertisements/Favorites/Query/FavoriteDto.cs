using ShagOxServer.Application.DTOs.Advertisements.Core.Query.Core;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Advertisements.Favorites.Query;
public sealed record FavoriteDto
(
    long Id,
    EntityStatus EntityStatus,
    UserDto User,
    AdvertisementShortDto Advertisement
) : BaseDto(Id, EntityStatus);
