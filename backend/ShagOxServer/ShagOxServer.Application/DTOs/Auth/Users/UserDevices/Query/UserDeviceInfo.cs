namespace ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Query;
public sealed record UserDeviceInfo
(
    string DeviceName,
    string DeviceType,
    string Browser,
    string OperatingSystem,
    string IpAddress,
    string DeviceIdentifier
);
