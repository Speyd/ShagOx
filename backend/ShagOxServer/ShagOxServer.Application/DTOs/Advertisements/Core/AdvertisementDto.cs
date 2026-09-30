using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.DTOs.Advertisements.Statuses;
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
    StatusDto Status,
    CurrencyDto Currency,
    CategoryDto Category,
    UserShortDto Seller,
    UserShortDto? Buyer,
    List<ImageDto> Images,
    JsonDocument Attributes,
    List<AdvertisementVariantDto> Variants,
    DateTime? SoldAt,
    DateTime CreatedAt
) : BaseDto(Id);