using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Domain.Cache.Dictionaries;
public sealed record CategoryCacheContext
(
    long Id,
    long ProductTypeId,
    EntityStatus Status = EntityStatus.Active
);
