namespace ShagOxServer.Application.DTOs.Devices;
public sealed record DeviceInfo
(
    string DeviceName,
    string DeviceType,
    string Browser,
    string OperatingSystem,
    string DeviceIdentifier
);
