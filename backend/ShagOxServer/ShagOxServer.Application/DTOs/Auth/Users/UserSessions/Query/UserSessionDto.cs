using ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;

namespace ShagOxServer.Application.DTOs.Auth.Users.UserSessions.Query;
public sealed record UserSessionDto
(
    long Id,
    EntityStatus EntityStatus,
    long OwnerId,
    UserDeviceDto UserDevice,
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
