using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Specification.Conditions.Cache;
public sealed record ConditionCacheInfo
(
    long Id,
    EntityStatus Status = EntityStatus.Active
);
