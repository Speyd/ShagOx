using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Advertisements.Statuses.Cache;
public sealed record StatusCacheInfo
(
    long Id,
    EntityStatus Status = EntityStatus.Active
);
