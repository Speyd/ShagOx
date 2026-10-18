using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update;

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
    List<OwnerPictureOrderUpdateRequest>? Pictures
);
