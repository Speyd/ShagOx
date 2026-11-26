namespace ShagOxServer.Application.DTOs.Devices;
public sealed record DeviceInfo
(
    string DeviceName,
    string DeviceType,
    string OperatingSystem,
    string DeviceIdentifier
);
