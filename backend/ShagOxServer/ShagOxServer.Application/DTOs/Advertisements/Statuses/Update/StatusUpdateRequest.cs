namespace ShagOxServer.Application.DTOs.Advertisements.Statuses.Update;
public sealed record StatusUpdateRequest
(
    string? Code,
    string? Name,
    string? Description
);