namespace ShagOxServer.Domain.Filters.Devices;
public sealed record DeviceSearchFilter
(
    string? DeviceName,
    string? DeviceType,
    string? Browser,
    string? OperatingSystem
) : BaseFilter();
