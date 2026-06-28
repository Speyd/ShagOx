
namespace ShagOxServer.Application.DTOs.Roles.Update;
public sealed record RoleUpdateRequest
(
    string? Name = null,
    string? Description = null
);