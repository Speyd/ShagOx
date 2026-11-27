using ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.DTOs.Devices.Query;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Application.Services.Auth.Users.UserDevices.Mapping;
public static class UserDeviceMapper
{
    public static UserDeviceDto ToDto(
        UserDevice userDevice,
        DeviceDto deviceDto)
    {
        return new UserDeviceDto(
            userDevice.Id,
            userDevice.EntityStatus,
            userDevice.Status,
            userDevice.OwnerId,
            deviceDto,
            userDevice.CreatedAt,
            userDevice.LastActiveAt,
            userDevice.IsTrusted
        );
    }
}
