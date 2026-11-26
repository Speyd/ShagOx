using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Domain.Cache.Auth.User;
public sealed record UserDeviceCacheContext
(
    long Id,
    long OwnerId,
    string? IpAddress,
    string Identifier,
    EntityStatus Status = EntityStatus.Active
);
