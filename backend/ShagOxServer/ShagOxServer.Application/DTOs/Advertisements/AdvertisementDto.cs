using ShagOxServer.Application.DTOs.Auth.Users;
using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.DTOs.Dictionaries.Categories;
using ShagOxServer.Application.DTOs.Specification.Currencies;
using ShagOxServer.Application.DTOs.Specification.Pictures.Images;

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
) : BaseDto(Id);