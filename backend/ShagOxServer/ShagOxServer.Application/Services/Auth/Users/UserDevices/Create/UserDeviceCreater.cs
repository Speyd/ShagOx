using ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Create;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;

namespace ShagOxServer.Application.Services.Auth.Users.UserDevices.Create;
public static class UserDeviceCreater
{
    public static UserDevice Create(
       UserDeviceCreateRequest request)
    {
        return new UserDevice
        {
            OwnerId = request.OwnerId,
            DeviceId = request.DeviceId,
            CreatedAt = DateTime.UtcNow,
            Status = UserDeviceStatus.Active,
            IsTrusted = false,
        };
    }
}
