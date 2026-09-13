using ShagOxServer.Application.DTOs.Auth.Roles;
using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.DTOs.Location.Cities;
using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars;

namespace ShagOxServer.Application.DTOs.Auth.Users;
public sealed record UserDto
(
    int Id,
    string? FirstName,
    string? LastName,
    string UserName,
    string? Bio,
    string? Phone,
    string? Email,

    AvatarDto? Avatar,

    CityDto City,

    List<RoleDto> Roles,

    DateTime? LastSeenAt,
    DateTime RegisteredAt
) : BaseDto(Id);