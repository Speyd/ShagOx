using ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Create;
using ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Query;

namespace ShagOxServer.Application.Interfaces.Providers;
public interface IDeviceInfoProvider
{
    UserDeviceInfo GetDeviceInfo(
        string deviceIdentifier);
}
