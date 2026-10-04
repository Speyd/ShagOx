using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Dictionaries.Categories.Query;
public sealed record CategoryDto
(
    long Id,
    string Code,
    CategoryProductTypeDto ProductType,
    string? Lable
) : BaseDto(Id);
