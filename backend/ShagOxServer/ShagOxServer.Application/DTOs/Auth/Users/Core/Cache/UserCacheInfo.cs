using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Auth.Users.Core.Cache;
public sealed record UserCacheInfo
(
    long Id,
    string? Email,
    string? Phone,
    string UserName,
    EntityStatus Status = EntityStatus.Active
);
