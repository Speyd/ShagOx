using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Query;
public sealed record UserDeviceDto
(
    long Id,
    EntityStatus EntityStatus,
    long OwnerId,
    string DeviceName,
    string DeviceType,
    string Browser,
    string OperatingSystem,
    string IpAddress,
    string DeviceIdentifier
) : BaseDto(Id, EntityStatus);
