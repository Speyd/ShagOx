using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Dictionaries.Categories.Query;
public sealed record CategoryDto
(
    long Id,
    string Code,
    long ProductTypeId,
    string? Lable
) : BaseDto(Id);
