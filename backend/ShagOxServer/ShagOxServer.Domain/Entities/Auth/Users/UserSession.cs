using ShagOxServer.Domain.Base.Owner;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;

namespace ShagOxServer.Domain.Entities.Auth.Users;
public class UserSession
    : OwnedEntity<User>
{
    public long DeviceId { get; set; }
    public UserDevice Device { get; set; } = null!;

    public string RefreshTokenHash { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime? RevokedAt { get; set; }

    public SessionRevocationReason? RevocationReason { get; set; }
    public DateTime LastUsedAt { get; set; }

    public bool IsActive =>
        RevokedAt == null && ExpiresAt > DateTime.UtcNow;

    public override string ToString()
    {
        return $"User: {OwnerId} | Device: {DeviceId}";
    }
}
