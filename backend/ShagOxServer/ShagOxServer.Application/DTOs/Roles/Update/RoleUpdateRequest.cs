
namespace ShagOxServer.Application.DTOs.Roles.Update;
public sealed record RoleUpdateRequest
(
    int Id,
    string? Name = null,
    string? Description = null
);