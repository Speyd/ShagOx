using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Domain.Cache.Devices;
public sealed record DeviceCacheContext
(
    long Id,
    string? IpAddress,
    string Identifier,
    EntityStatus Status = EntityStatus.Active
);
