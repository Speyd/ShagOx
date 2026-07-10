namespace ShagOxServer.Application.DTOs.Specification.Images.Create;
public sealed record ImageCreateResponse
(
    int Id,
    string PublicId,
    DateTime CreatedAt
);