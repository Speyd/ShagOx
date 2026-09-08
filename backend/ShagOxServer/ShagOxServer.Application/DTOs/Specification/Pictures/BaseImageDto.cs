using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Specification.Pictures;
public abstract record BaseImageDto
(
    int Id,
    string Url,
    string PublicId
) : BaseDto(Id);