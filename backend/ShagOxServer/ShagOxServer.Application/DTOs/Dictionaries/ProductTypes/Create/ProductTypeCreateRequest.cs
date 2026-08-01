namespace ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Create;
public sealed record ProductTypeCreateRequest
(
    string Name,
    string Description = ""
);