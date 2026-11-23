namespace ShagOxServer.Domain.Filters.Auth.Users.UserDevices;
public sealed record UserDeviceSearchFilter
(
    long? UserId,
    string? DeviceName,
    string? DeviceType,
    string? Browser,
    string? OperatingSystem,
    bool? IsTrusted
) : BaseFilter();
