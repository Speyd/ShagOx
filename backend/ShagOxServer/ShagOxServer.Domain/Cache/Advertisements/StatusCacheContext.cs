using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Domain.Cache.Advertisements;
public sealed record StatusCacheContext
(
    long Id,
    EntityStatus Status = EntityStatus.Active
);
