namespace ShagOxServer.Application.DTOs.Dictionaries.Categories.Update;
public sealed record CategoryUpdateRequest
(
    string? Code,
    long? ProductTypeId,
    List<long>? Attributes,
    List<long>? Advertisements
);