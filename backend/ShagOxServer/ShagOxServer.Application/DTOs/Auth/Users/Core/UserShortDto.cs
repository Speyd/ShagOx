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

    long? AvatarId,

    long? CityId,

    DateTime? LastSeenAt
) : BaseDto(Id);