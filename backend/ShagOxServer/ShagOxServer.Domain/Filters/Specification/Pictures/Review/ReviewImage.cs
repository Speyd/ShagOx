namespace ShagOxServer.Domain.Filters.Specification.Pictures.Review;
public sealed record ReviewImageSearchFilter
(
    long? AdvertisementId,
    string? PublicId,
    int? Order
) : PictureSearchFilter(PublicId);
