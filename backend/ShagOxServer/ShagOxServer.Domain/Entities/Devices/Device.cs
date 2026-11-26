using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Cache.Devices;
using ShagOxServer.Domain.Entities.Devices.Enum;

namespace ShagOxServer.Domain.Entities.Devices;
public class Device
    : BaseEntity,
    IEntityCache<Device, DeviceCacheContext>
{
    public string DeviceName { get; set; } = null!;

    public string DeviceType { get; set; } = null!;

    public string? Browser { get; set; }

    public string? OperatingSystem { get; set; }

    public string DeviceIdentifier { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime LastActiveAt { get; set; }

    public DeviceStatus Status { get; set; }
    public DateTime? BlockedAt { get; set; }
    public DateTime? BlockedUntil { get; set; }

    public DeviceCacheContext GetCacheContext()
    {
        return new DeviceCacheContext(
            Id,
            IpAddress,
            DeviceIdentifier,
            EntityStatus);
    }

    public override string ToString()
    {
        return $"DeviceName: {DeviceName} | DeviceType:{DeviceType}";
    }

    object IEntityCache.GetCacheContext()
    {
        return GetCacheContext();
    }
}
