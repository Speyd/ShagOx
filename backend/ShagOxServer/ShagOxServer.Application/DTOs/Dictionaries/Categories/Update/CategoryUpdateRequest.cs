namespace ShagOxServer.Application.DTOs.Dictionaries.Categories.Update;
public sealed record CategoryUpdateRequest
(
    string? Name,
    int? ProductTypeId,
    List<int>? Attributes,
    List<int>? Advertisements
);