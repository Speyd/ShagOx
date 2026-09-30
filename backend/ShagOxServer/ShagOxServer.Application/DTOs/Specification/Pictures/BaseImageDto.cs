using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Specification.Pictures;
public abstract record BaseImageDto
(
    long Id,
    string Url,
    string PublicId
) : BaseDto(Id);