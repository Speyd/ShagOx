using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Domain.Cache.Advertisements;
public sealed record FavoriteCacheContext
(
    long Id,
    long UserId,
    EntityStatus Status = EntityStatus.Active
);
