using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Devices.Query;
public sealed record DeviceDto
(
    long Id,
    EntityStatus EntityStatus,
    string DeviceName,
    string DeviceType,
    string? OperatingSystem,
    string DeviceIdentifier
) : BaseDto(Id, EntityStatus);
