using ShagOxServer.Domain.Base.Owner;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;

namespace ShagOxServer.Domain.Entities.Auth.Users;
public class UserDevice
    : OwnedEntity<User>
{
    public string DeviceName { get; set; } = null!;

    public string DeviceType { get; set; } = null!;

    public string? Browser { get; set; }

    public string? OperatingSystem { get; set; }

    public string? IpAddress { get; set; }

    public string DeviceIdentifier { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime LastActiveAt { get; set; }

    public UserDeviceStatus Status { get; set; }
    public DateTime? BlockedAt { get; set; }
    public DateTime? BlockedUntil { get; set; }

    public bool IsTrusted { get; set; }


    public override string ToString()
    {
        return $"User: {OwnerId} | IsTrusted:{IsTrusted}";
    }
}
