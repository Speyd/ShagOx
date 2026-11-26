using ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Query;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Application.Services.Auth.Users.UserDevices.Mapping;
public static class UserDeviceMapper
{
    public static UserDeviceDto ToDto(
        UserDevice device)
    {
        return new UserDeviceDto(
            device.Id,
            device.EntityStatus,
            device.OwnerId,
            device.DeviceName,
            device.DeviceType,
            device.Browser,
            device.OperatingSystem,
            device.IpAddress,
            device.DeviceIdentifier
        );
    }
}
