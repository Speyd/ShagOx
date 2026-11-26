using ShagOxServer.Application.DTOs.Devices;

namespace ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Query;
public sealed record UserDeviceInfo
(
    DeviceInfo DeviceInfo,
    string IpAddress,
);
