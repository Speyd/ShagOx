using ShagOxServer.Application.DTOs.Devices;

namespace ShagOxServer.Application.Interfaces.Providers.Devices;
public interface IDeviceInfoProvider
{
    DeviceInfo GetDeviceInfo(
        string deviceIdentifier);
}
