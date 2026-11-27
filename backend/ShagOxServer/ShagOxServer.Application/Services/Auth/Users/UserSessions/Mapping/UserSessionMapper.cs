using ShagOxServer.Application.DTOs.Auth.Users.UserSessions.Query;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Application.Services.Auth.Users.UserSessions.Mapping;
public static class UserSessionMapper
{
    public static UserSessionDto ToDto(
        UserSession session)
    {
        return new UserSessionDto(
            session.Id,
            session.EntityStatus,
            session.OwnerId,
            session.UserDeviceId,
            session.Browser,
            session.BrowserVersion,
            session.IpAddress,
            session.CreatedAt,
            session.ExpiresAt,
            session.RevokedAt,
            session.LastUsedAt,
            session.IsActive,
            session.RevocationReason
        );
    }
}
