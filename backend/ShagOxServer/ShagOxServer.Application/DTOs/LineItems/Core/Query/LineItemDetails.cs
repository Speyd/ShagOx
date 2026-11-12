using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.DTOs.LineItems.Core.Query;
public record LineItemDetails
(
    int Stock,
    decimal? Price,
    Advertisement? Advertisement,
    AdvertisementVariant? Variant
);
