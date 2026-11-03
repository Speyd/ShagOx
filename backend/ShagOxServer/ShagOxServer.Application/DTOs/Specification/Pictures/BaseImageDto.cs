using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Specification.Pictures;
public abstract record BaseImageDto
(
    long Id,
    EntityStatus EntityStatus,
    string Url,
    string PublicId
) : BaseDto(Id, EntityStatus);
