using ShagOxServer.Domain.Entities.Dictionaries.Enum;

namespace ShagOxServer.Application.DTOs.Dictionaries.Categories.Update;
public sealed record CategoryUpdateRequest
(
    string? Name,
    ProductType? ProductType,
    List<int> Attributes,
    List<int> Advertisements
);
