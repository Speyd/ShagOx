using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Auth.Roles;
public sealed record RoleDto
(
    int Id,
    string Name,
    string Description
) : BaseDto(Id);