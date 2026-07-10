using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Services.Advertisements.Update;
public sealed record AdvertisementUpdateValidationResult(
    User? Buyer,
    Currency? Currency,
    Condition? Condition,
    Category? Category,
    List<Image>? Images
);