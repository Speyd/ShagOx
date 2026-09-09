using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Dictionaries.Categories;
public sealed record CategoryDto
(
    int Id,
    string Name,
    CategoryProductTypeDto ProductType
) : BaseDto(Id);
