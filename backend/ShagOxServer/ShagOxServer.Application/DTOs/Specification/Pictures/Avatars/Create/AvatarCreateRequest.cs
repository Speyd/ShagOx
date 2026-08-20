using Microsoft.AspNetCore.Http;

namespace ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Create;
public sealed record AvatarCreateRequest
(
    IFormFile File,
    int UserId
);