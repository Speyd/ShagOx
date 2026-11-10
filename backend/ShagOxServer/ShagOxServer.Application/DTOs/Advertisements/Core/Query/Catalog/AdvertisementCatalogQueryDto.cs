using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.DTOs.Advertisements.Core.Query.Catalog;
public sealed record AdvertisementCatalogQueryDto
(
    Advertisement Advertisement,

    int ReviewCount,

    float AverageRating
);
