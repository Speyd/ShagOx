using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Auth.Roles.Query;
public sealed record RoleDto
(
    long Id,
    string Name,
    string Description
) : BaseDto(Id);