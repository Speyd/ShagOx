using ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.DTOs.Devices;

namespace ShagOxServer.Application.Interfaces.Providers.User;
public interface IDeviceInfoProvider
{
    UserDeviceInfo GetDeviceUserInfo(
        string deviceIdentifier);

    DeviceInfo GetDeviceInfo(
        string deviceIdentifier);
}
