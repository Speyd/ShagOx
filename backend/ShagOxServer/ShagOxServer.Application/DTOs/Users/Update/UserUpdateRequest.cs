
namespace ShagOxServer.Application.DTOs.Users.Update;
public sealed record UserUpdateRequest
(
    string? Surname,
    string? Name,
    string? Phone,
    string? Email,
    string? Avatar,
    int? CityId
);
