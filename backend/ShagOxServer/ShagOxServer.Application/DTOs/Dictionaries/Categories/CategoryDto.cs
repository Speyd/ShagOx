using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Dictionaries.Categories;
public sealed record CategoryDto
(
    long Id,
    string Name,
    CategoryProductTypeDto ProductType,
    string? Lable
) : BaseDto(Id);
