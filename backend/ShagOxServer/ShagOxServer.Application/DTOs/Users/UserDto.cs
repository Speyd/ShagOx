
namespace ShagOxServer.Application.DTOs.Users;
public sealed record UserDto
(
    int Id,
    string? Surname,
    string? Name,
    string? Phone,
    string? Email,
    string? Avatar,

    int? CityId,
    string? CityName,

    List<int> RolesId,
    List<string> RolesName,

    DateTime? LastSeenAt,
    DateTime RegisteredAt
);