using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Auth.UserRoles;
public sealed record UserRoleDto
(
    long Id,
    long UserId,
    long RoleId
) : BaseDto(Id);