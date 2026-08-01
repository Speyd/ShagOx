namespace ShagOxServer.Application.DTOs.Auth.Roles.Update;
public sealed record RoleUpdateRequest
(
    string? Name = null,
    string? Description = null
);