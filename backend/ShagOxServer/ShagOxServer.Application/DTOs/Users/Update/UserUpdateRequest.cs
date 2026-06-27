
namespace ShagOxServer.Application.DTOs.Users.Create;
public sealed record UserUpdateRequest
(
    int Id,
    string? Surname,
    string? Name,
    string? Phone,
    string? Email,
    string? Avatar,
    int? CityId
);
