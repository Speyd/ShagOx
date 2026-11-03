using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Location.Cities.Cache;
public sealed record CityCacheInfo
(
    long Id,
    long RegionId,
    EntityStatus Status = EntityStatus.Active
);
