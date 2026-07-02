using ShagOxServer.Application.DTOs.Location.Cities;
using ShagOxServer.Application.DTOs.Roles;

namespace ShagOxServer.Application.DTOs.Users;
public sealed record UserDto
(
    int Id,
    string? Surname,
    string? Name,
    string? Phone,
    string? Email,
    string? Avatar,

    CityDto City,

    List<RoleDto> Roles,

    DateTime? LastSeenAt,
    DateTime RegisteredAt
);