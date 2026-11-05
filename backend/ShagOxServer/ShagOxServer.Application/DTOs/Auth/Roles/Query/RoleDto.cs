using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Auth.Roles.Query;
public sealed record RoleDto
(
    long Id,
    EntityStatus EntityStatus,
    string Name,
    string Description
) : BaseDto(Id, EntityStatus);
