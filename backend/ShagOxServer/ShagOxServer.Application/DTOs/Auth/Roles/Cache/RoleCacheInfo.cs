using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Auth.Roles.Cache;
public sealed record RoleCacheInfo
(
    long Id,
    string Name,
    EntityStatus Status = EntityStatus.Active
);
