using ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Query;

namespace ShagOxServer.Application.Interfaces.Providers.User;
public interface IDeviceInfoProvider
{
    UserDeviceInfo GetDeviceInfo(
        string deviceIdentifier);
}
