namespace ShagOxServer.Domain.Filters.Advertisements;
public sealed record FavoriteSearchFilter
(
     long? UserId,
     long? AdvertisementId
) : BaseFilter();