namespace ShagOxServer.Domain.Filters.Specification.Pictures.Review;
public sealed record ReviewRepliImageSearchFilter
(
    long? ReviewId,
    string? PublicId,
    int? Order
) : PictureSearchFilter(PublicId);
