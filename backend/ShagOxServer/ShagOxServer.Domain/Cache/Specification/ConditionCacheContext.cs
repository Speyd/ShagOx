using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Domain.Cache.Specification;
public sealed record ConditionCacheContext
(
    long Id,
    EntityStatus Status = EntityStatus.Active
);
