namespace ShagOxServer.Domain.Filters.Auth.Users.UserDevices;
public sealed record UserDeviceSearchFilter
(
    long? UserId,
    long? DeviceId,
    bool? IsTrusted
) : BaseFilter();
