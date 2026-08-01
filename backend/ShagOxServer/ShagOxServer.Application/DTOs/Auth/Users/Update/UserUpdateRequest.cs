namespace ShagOxServer.Application.DTOs.Auth.Users.Update;
public sealed record UserUpdateRequest
(
    string? Surname,
    string? Name,
    string? Phone,
    string? Email,
    string? Avatar,
    int? CityId
);