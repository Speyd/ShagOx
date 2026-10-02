using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Update;
using ShagOxServer.Application.DTOs.Advertisements.Core.Update.Images;

namespace ShagOxServer.Application.DTOs.Advertisements.Core.Update;
public sealed record AdvertisementUpdateRequest
(
    string? Title,
    string? Description,
    int? Popularity,
    long? CurrencyId,
    long? ConditionId,
    long? CategoryId,
    long? BuyerId,
    string? Attributes,
    string? Variants,
    List<ImageAdvertUpdateRequest>? Images
);