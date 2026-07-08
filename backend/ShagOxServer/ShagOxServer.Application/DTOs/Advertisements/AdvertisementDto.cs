using ShagOxServer.Application.DTOs.Dictionaries.Categories;
using ShagOxServer.Application.DTOs.Specification.Currencies;
using ShagOxServer.Application.DTOs.Specification.Images;
using ShagOxServer.Application.DTOs.Users;

namespace ShagOxServer.Application.DTOs.Advertisements;

public sealed record AdvertisementDto
(
    int Id,
    string Title,
    string Description,
    int Price,
    int PreviousPrice,
    CurrencyDto Currency,
    CategoryDto Category,
    UserShortDto Seller,
    UserShortDto? Buyer,
    List<ImageDto> Images,
    Dictionary<string, string> Properties,
    DateTime? SoldAt,
    DateTime CreatedAt
);