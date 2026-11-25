using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Domain.Cache.Dictionaries;
public sealed record ProductTypeCacheContext
(
    long Id,
    EntityStatus Status = EntityStatus.Active
);
