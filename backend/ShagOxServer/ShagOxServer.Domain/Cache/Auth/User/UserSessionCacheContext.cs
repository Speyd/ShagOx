using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Domain.Cache.Auth.User;
public sealed record UserSessionCacheContext
(
    long Id,
    long OwnerId,
    long UserDeviceId,
    string? IpAddress,
    EntityStatus Status = EntityStatus.Active
);
