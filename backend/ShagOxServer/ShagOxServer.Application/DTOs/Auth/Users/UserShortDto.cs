using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Auth.Users;
public sealed record UserShortDto
(
    int Id,
    string? FirstName,
    string? LastName,
    string? UserName,
    string? Bio,
    string? Phone,
    string? Email,

    int? Avatar,

    int? CityId,
    string? CityName,

    DateTime? LastSeenAt
) : BaseDto(Id);