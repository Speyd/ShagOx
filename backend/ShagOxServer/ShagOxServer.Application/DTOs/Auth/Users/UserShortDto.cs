namespace ShagOxServer.Application.DTOs.Auth.Users;

public sealed record UserShortDto
(
    int Id,
    string? Surname,
    string? Name,
    string? Phone,
    string? Email,
    //string? Avatar,

    int? CityId,
    string? CityName,

    DateTime? LastSeenAt
);
