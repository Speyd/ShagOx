using ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Create;
using ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.DTOs.Devices;
using ShagOxServer.Application.DTOs.Devices.Create;
using ShagOxServer.Domain.Entities.Devices;
using ShagOxServer.Domain.Entities.Devices.Enum;
namespace ShagOxServer.Application.Services.Devices.Create;
public static class DeviceCreater
{
    public static Device Create(
       DeviceCreateRequest request,
       DeviceInfo deviceInfo)
    {
        return new Device
        {
            DeviceName = deviceInfo.DeviceName,
            DeviceType = deviceInfo.DeviceType,
            OperatingSystem = deviceInfo.OperatingSystem,
            DeviceIdentifier = deviceInfo.DeviceIdentifier,
            CreatedAt = DateTime.UtcNow,
            LastActiveAt = DateTime.UtcNow,
            Status = DeviceStatus.Active,
        };
    }
}
