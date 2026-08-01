namespace ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Update;
public sealed record ProductTypeUpdateRequest
(
    string? Name,
    string? Description
);