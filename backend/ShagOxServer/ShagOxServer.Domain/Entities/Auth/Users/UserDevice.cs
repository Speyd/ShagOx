using ShagOxServer.Domain.Base.Owner;

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

    public bool IsTrusted { get; set; }


    public override string ToString()
    {
        return $"User: {OwnerId} | IsTrusted:{IsTrusted}";
    }
}
