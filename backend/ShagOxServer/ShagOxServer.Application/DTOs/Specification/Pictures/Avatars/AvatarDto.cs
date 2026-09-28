namespace ShagOxServer.Application.DTOs.Specification.Pictures.Avatars;
public sealed record AvatarDto(
    long Id,
    string Url,
    string PublicId,
    long UserId
) : BaseImageDto(Id, Url, PublicId);