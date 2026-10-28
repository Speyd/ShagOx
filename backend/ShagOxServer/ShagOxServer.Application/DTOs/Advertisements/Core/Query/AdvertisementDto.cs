using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.DTOs.Advertisements.Statuses.Query;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.CategoryAttributes;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Query;
using ShagOxServer.Application.DTOs.Specification.Currencies.Query;
using ShagOxServer.Application.DTOs.Specification.Pictures.AdvertisementImages.Query;

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
    List<AdvertisementImageDto> Pictures,
    List<AttributeDto> Attributes,
    List<AdvertisementVariantDto> Variants,
    DateTime? SoldAt,
    DateTime CreatedAt
) : BaseDto(Id);
