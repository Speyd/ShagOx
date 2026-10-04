using ShagOxServer.Application.DTOs.Auth.Roles.Query;
using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.DTOs.Location.Cities.Query;
using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Query;

namespace ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
public sealed record UserDto
(
    long Id,
    string? FirstName,
    string? LastName,
    string UserName,
    string? Bio,
    string? Phone,
    string? Email,

    AvatarDto? Avatar,

    CityDto? City,

    List<RoleDto> Roles,

    DateTime? LastSeenAt,
    DateTime RegisteredAt
) : BaseDto(Id);