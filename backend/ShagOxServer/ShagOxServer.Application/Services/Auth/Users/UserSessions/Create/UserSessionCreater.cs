using ShagOxServer.Application.DTOs.Auth.Users.UserSessions;
using ShagOxServer.Application.DTOs.Auth.Users.UserSessions.Create;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Application.Services.Auth.Users.UserSessions.Create;
public static class UserSessionCreater
{
    public static UserSession Create(
       UserSessionCreateRequest request,
       SessionInfo info,
       int expireDays,
       string tokenHash)
    {
        var now = DateTime.UtcNow;

        return new UserSession
        {
            OwnerId = request.OwnerId,
            UserDeviceId = request.UserDeviceId,
            RefreshTokenHash = tokenHash,
            IpAddress = info.IpAddress,
            Browser = info.Browser,
            BrowserVersion = info.BrowserVerison,
            ExpiresAt = now.AddDays(expireDays),
            CreatedAt = now,
            LastUsedAt = now,
        };
    }
}
