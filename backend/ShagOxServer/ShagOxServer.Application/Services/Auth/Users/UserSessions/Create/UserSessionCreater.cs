using ShagOxServer.Application.DTOs.Auth.Users.UserSessions.Create;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Application.Services.Auth.Users.UserSessions.Create;
public static class UserSessionCreater
{
    public static UserSession Create(
       UserSessionCreateRequest request,
       int expireDays,
       string tokenHash)
    {
        var now = DateTime.UtcNow;

        return new UserSession
        {
            OwnerId = request.OwnerId,
            DeviceId = request.DeviceId,
            RefreshTokenHash = tokenHash,
            ExpiresAt = now.AddDays(expireDays),
            CreatedAt = now,
            LastUsedAt = now,
        };
    }
}
