using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Auth.Users.Core;
public sealed record UserShortDto
(
    long Id,
    string? FirstName,
    string? LastName,
    string? UserName,
    string? Bio,
    string? Phone,
    string? Email,

    long? Avatar,

    long? CityId,
    string? CityName,

    DateTime? LastSeenAt
) : BaseDto(Id);