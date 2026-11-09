using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.DTOs.Advertisements.Core.Query;
public sealed record AdvertisementCatalogQueryDto
(
    Advertisement Advertisement,

    int ReviewCount,

    float AverageRating
);
