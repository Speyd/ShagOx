using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Location.Regions.Cache;
public sealed record RegionCacheInfo
(
    long Id,
    EntityStatus Status = EntityStatus.Active
);
