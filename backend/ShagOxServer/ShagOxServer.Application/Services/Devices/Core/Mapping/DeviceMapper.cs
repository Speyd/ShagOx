using ShagOxServer.Application.DTOs.Devices.Query;
using ShagOxServer.Domain.Entities.Devices;

namespace ShagOxServer.Application.Services.Devices.Core.Mapping;
public static class DeviceMapper
{
    public static DeviceDto ToDto(
        Device device)
    {
        return new DeviceDto(
            device.Id,
            device.EntityStatus,
            device.DeviceName,
            device.DeviceType,
            device.OperatingSystem,
            device.DeviceIdentifier
        );
    }
}
