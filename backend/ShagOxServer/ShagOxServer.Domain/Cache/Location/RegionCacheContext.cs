using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Domain.Cache.Location;
public sealed record RegionCacheContext
(
    long Id,
    EntityStatus Status = EntityStatus.Active
);
