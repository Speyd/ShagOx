using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Base.Owner;
using ShagOxServer.Domain.Cache.Auth.User;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;

namespace ShagOxServer.Domain.Entities.Auth.Users;
public class UserSession
    : OwnedEntity<User>,
    IEntityCache<UserSession, UserSessionCacheContext>
{
    public long UserDeviceId { get; set; }
    public UserDevice UserDevice { get; set; } = null!;

    public string RefreshTokenHash { get; set; } = null!;


    public string Browser { get; set; } = "";
    public string? BrowserVersion { get; set; }
    public string? IpAddress { get; set; }


    public DateTime CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime? RevokedAt { get; set; }

    public SessionRevocationReason? RevocationReason { get; set; }
    public DateTime LastUsedAt { get; set; }

    public bool IsActive =>
        RevokedAt == null && ExpiresAt > DateTime.UtcNow;


    public UserSessionCacheContext GetCacheContext()
    {
        return new UserSessionCacheContext(
            Id,
            OwnerId,
            UserDeviceId,
            IpAddress,
            EntityStatus);
    }

    public override string ToString()
    {
        return $"User: {OwnerId} | UserDevice: {UserDeviceId}";
    }

    object IEntityCache.GetCacheContext()
    {
        return GetCacheContext();
    }
}
