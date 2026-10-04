using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.DTOs.Advertisements.Statuses.Query;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Query;
using ShagOxServer.Application.DTOs.Specification.Currencies.Query;
using ShagOxServer.Application.DTOs.Specification.Pictures.Images.Query;
using System.Text.Json;

namespace ShagOxServer.Application.DTOs.Advertisements.Core.Query;
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