using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Advertisements.Favorites.Cache;
public sealed record FavoriteCacheInfo
(
    long Id,
    long UserId,
    EntityStatus Status = EntityStatus.Active
);
