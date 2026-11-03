using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Base;
public record BaseDto
(
    long Id,
    EntityStatus EntityStatus
);
