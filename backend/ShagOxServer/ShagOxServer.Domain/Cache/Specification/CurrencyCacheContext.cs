using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Domain.Cache.Specification;
public sealed record CurrencyCacheContext
(
    long Id,
    string Code,
    string Symbol,
    EntityStatus Status = EntityStatus.Active
);
