
namespace ShagOxServer.Application.DTOs.Roles.Update;
public sealed record RoleUpdateRequest
(
    int Id,
    string? Title = null,
    string? Description = null
);