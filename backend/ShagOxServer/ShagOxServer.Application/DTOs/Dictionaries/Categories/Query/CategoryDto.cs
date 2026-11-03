using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Dictionaries.Categories.Query;
public sealed record CategoryDto
(
    long Id,
    EntityStatus EntityStatus,
    string Code,
    long ProductTypeId,
    string? Label
) : BaseDto(Id, EntityStatus);
