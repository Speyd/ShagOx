namespace ShagOxServer.Domain.Filters.Specification.Pictures.AdvertisementImages;
public sealed record AdvertisementImageSearchFilter
(
    long? AdvertisementId,
    string? PublicId,
    int? Order
) : PictureSearchFilter(PublicId);
