namespace ShagOxServer.Domain.Filters.Specification.Pictures.Images;
public sealed record ImageSearchFilter
(
    long? AdvertisementId,
    string? PublicId,
    int? Order
) : PictureSearchFilter(PublicId);