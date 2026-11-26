using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;

namespace ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Query;
public sealed record UserDeviceDto
(
    long Id,
    EntityStatus EntityStatus,
    UserDeviceStatus Status,
    long OwnerId,

    long DeviceId,
    string DeviceName,
    string DeviceType,
    string? OperatingSystem,
    string DeviceIdentifier,

    DateTime CreatedAt,
    DateTime LastSeenAt,
    bool IsTrusted
) : BaseDto(Id, EntityStatus);
