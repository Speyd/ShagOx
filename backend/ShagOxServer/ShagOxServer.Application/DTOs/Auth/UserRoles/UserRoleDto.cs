namespace ShagOxServer.Application.DTOs.Auth.UserRoles;

public sealed record UserRoleDto
(
    int Id,
    int UserId,
    int RoleId
);