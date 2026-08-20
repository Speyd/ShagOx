namespace ShagOxServer.Application.DTOs.Specification.Pictures.Avatars;
public sealed record AvatarDto(
    int Id,
    string Url,
    string PublicId,
    int UserId
) : BaseImageDto(Id, Url, PublicId);