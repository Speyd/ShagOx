using Microsoft.AspNetCore.Http;

namespace ShagOxServer.Application.DTOs.Auth.Users.Core.Update;
public sealed record UserUpdateRequest
(
    string? FirstName,
    string? LastName,
    string? UserName,
    string? Bio,
    IFormFile? Avatar,
    int? CityId
);