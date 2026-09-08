using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Auth.UserRoles;
public sealed record UserRoleDto
(
    int Id,
    int UserId,
    int RoleId
) : BaseDto(Id);