using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Domain.Cache.Location;
public sealed record CityCacheContext
(
    long Id,
    long RegionId,
    EntityStatus Status = EntityStatus.Active
);
