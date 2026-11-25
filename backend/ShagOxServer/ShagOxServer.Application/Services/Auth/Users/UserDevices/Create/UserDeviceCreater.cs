using ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Create;
using ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Query;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;

namespace ShagOxServer.Application.Services.Auth.Users.UserDevices.Create;
public static class UserDeviceCreater
{
    public static UserDevice Create(
       UserDeviceCreateRequest request,
       UserDeviceInfo deviceInfo)
    {
        return new UserDevice
        {
            OwnerId = request.OwnerId,
            DeviceName = deviceInfo.DeviceName,
            DeviceType = deviceInfo.DeviceType,
            Browser = deviceInfo.Browser,
            OperatingSystem = deviceInfo.OperatingSystem,
            IpAddress = deviceInfo.IpAddress,
            DeviceIdentifier = deviceInfo.DeviceIdentifier,
            CreatedAt = DateTime.UtcNow,
            Status = UserDeviceStatus.Active,
            IsTrusted = false,
        };
    }
}
