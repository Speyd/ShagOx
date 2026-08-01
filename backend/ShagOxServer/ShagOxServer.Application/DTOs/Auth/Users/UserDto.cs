using ShagOxServer.Application.DTOs.Auth.Roles;
using ShagOxServer.Application.DTOs.Location.Cities;

namespace ShagOxServer.Application.DTOs.Auth.Users;
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