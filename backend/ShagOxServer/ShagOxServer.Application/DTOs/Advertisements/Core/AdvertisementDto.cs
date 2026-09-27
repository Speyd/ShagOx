using ShagOxServer.Application.DTOs.Auth.Users.Core;
using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.DTOs.Dictionaries.Categories;
using ShagOxServer.Application.DTOs.Specification.Currencies;
using ShagOxServer.Application.DTOs.Specification.Pictures.Images;
using System.Text.Json;

namespace ShagOxServer.Application.DTOs.Advertisements.Core;
public sealed record AdvertisementDto
(
    long Id,
    string Title,
    string Description,
    CurrencyDto Currency,
    CategoryDto Category,
    UserShortDto Seller,
    UserShortDto? Buyer,
    List<ImageDto> Images,
    JsonDocument Attributes,
    DateTime? SoldAt,
    DateTime CreatedAt
) : BaseDto(Id);