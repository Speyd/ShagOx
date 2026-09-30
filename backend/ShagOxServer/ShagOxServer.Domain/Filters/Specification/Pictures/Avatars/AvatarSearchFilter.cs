namespace ShagOxServer.Domain.Filters.Specification.Pictures.Avatars;
public sealed record AvatarSearchFilter
(
    long? UserId,
    string? PublicId
) : PictureSearchFilter(PublicId);