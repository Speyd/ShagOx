using CloudinaryDotNet;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.DTOs.Advertisements.Statuses.Query;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.DTOs.Common.Currencies;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.Core.Query;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Query;
using ShagOxServer.Application.DTOs.Specification.Conditions.Query;
using ShagOxServer.Application.DTOs.Specification.Currencies.Query;
using ShagOxServer.Application.DTOs.Specification.Pictures.AdvertisementImages.Query;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Advertisements.Core.Query;
public sealed record AdvertisementDto
(
    long Id,
    EntityStatus EntityStatus,
    string Title,
    string Description,
    decimal? Price,
    decimal? PreviousPrice,
    ConvertedPricesDto PriceConversion,
    StatusDto Status,
    CurrencyDto Currency,
    CategoryDto Category,
    ConditionDto Condition,
    UserShortDto Seller,
    UserShortDto? Buyer,
    List<AdvertisementImageDto> Pictures,
    List<AttributeDto> Attributes,
    List<AdvertisementVariantDto> Variants,
    DateTime? SoldAt,
    DateTime CreatedAt
) : BaseDto(Id, EntityStatus);
