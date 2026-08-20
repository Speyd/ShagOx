namespace ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Update;
public sealed record AvatarUpdateRequest
(
    string? Url,
    int? UserId
);