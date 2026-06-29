using ShagOxServer.Domain.Entities.Dictionaries.Enum;

namespace ShagOxServer.Application.DTOs.Dictionaries.Categories;

public sealed record CategoryDto
(
    string Name,
    ProductType ProductType,
    List<int> Attributes,
    List<int> Advertisements
);