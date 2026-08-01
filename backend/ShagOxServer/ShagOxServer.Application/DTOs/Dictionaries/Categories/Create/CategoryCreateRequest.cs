namespace ShagOxServer.Application.DTOs.Dictionaries.Categories.Create;
public sealed record CategoryCreateRequest
(
    string Name,
    int ProductTypeId
);