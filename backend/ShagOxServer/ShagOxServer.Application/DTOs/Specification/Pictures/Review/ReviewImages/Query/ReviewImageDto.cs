namespace ShagOxServer.Application.DTOs.Specification.Pictures.Review.ReviewImages.Query;
public sealed record ReviewImageDto(
    long Id,
    string Url,
    string PublicId,
    int Order,
    long ReviewCommentId
) : BaseImageDto(Id, Url, PublicId);
