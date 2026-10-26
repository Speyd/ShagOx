namespace ShagOxServer.Application.DTOs.Specification.Pictures.Review.ReviewRepliImages.Query;
public sealed record ReviewRepliImageDto(
    long Id,
    string Url,
    string PublicId,
    int Order,
    long ReviewRepliCommentId
) : BaseImageDto(Id, Url, PublicId);
