using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Domain.Cache.Baskets;
public sealed record BasketCacheContext
(
    long Id,
    long UserId,
    EntityStatus Status = EntityStatus.Active
);
