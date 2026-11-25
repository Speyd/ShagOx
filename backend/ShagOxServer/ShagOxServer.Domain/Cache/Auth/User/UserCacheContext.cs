using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Domain.Cache.Auth.User;
public sealed record UserCacheContext
(
    long Id,
    string? Email,
    string? Phone,
    string UserName,
    EntityStatus Status = EntityStatus.Active
);
