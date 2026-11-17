using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.DTOs.Advertisements.Core.Query.Catalog;
public sealed record AdvertisementCatalogQueryDto
(
    Advertisement Advertisement,

    long ReviewCount,
    float AverageRating,

    bool IsFavorite,

    bool IsInBasket
);
