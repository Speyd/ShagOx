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
            device.Status,
            device.OwnerId,
            device.DeviceId,
            device.Device.DeviceName,
            device.Device.DeviceType,
            device.Device.OperatingSystem,
            device.Device.DeviceIdentifier,
            device.CreatedAt,
            device.LastActiveAt,
            device.IsTrusted
        );
    }
}
