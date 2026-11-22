using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.DTOs.Specification.Pictures.CategoryImages.Query;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Dictionaries.Categories.Query;
public sealed record CategoryDto
(
    long Id,
    EntityStatus EntityStatus,
    string Code,
    long ProductTypeId,
    CategoryImageDto? Picture,
    string? Label
) : BaseDto(Id, EntityStatus);
