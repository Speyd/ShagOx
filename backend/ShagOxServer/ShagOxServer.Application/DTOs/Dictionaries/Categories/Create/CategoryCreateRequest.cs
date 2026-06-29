using ShagOxServer.Domain.Entities.Dictionaries.Enum;

namespace ShagOxServer.Application.DTOs.Dictionaries.Categories.Create;
public sealed record CategoryCreateRequest
(
    string Name,
    ProductType ProductType,
    List<int> Attributes,
    List<int> Advertisements
);