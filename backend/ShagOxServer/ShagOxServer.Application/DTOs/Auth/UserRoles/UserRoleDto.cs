using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Auth.UserRoles;
public sealed record UserRoleDto
(
    long Id,
    EntityStatus EntityStatus,
    long UserId,
    long RoleId
) : BaseDto(Id, EntityStatus);
