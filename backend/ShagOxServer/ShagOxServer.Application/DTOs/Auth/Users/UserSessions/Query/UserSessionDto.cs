using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;

namespace ShagOxServer.Application.DTOs.Auth.Users.UserSessions.Query;
public sealed record UserSessionDto
(
    long Id,
    EntityStatus EntityStatus,
    long UserDeviceId,
    string Browser,
    string? BrowserVersion,
    string? IpAddress,
    DateTime CreatedAt,
    DateTime ExpiresAt,
    DateTime? RevokedAt,
    DateTime LastUsedAt,
    bool IsActive,
    SessionRevocationReason? RevocationReason
) : BaseDto(Id, EntityStatus);
