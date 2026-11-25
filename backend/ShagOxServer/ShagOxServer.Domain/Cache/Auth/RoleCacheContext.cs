using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Domain.Cache.Auth;
public sealed record RoleCacheContext
(
    long Id,
    string Name,
    EntityStatus Status = EntityStatus.Active
);
