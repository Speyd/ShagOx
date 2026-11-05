using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.DTOs.Advertisements.Core.Query;
using ShagOxServer.Application.DTOs.Advertisements.Statuses.Query;
using ShagOxServer.Application.DTOs.Common.Currencies;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.Core.Query;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Query;
using ShagOxServer.Application.DTOs.Specification.Currencies.Query;
using ShagOxServer.Application.Services.Auth.Users.Core.Mapping;
using ShagOxServer.Application.Services.Specification.Currencies.Mapping;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Mapping;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Core.Mapping;
public static class AdvertisementMapper
{
    public static AdvertisementDto ToDto(
        Advertisement x,
        List<AdvertisementVariantDto> variants,
        CategoryDto category,
        StatusDto status,
        List<AttributeDto> attributes,
        ConvertedPricesDto priceConversion)
    {
        return new AdvertisementDto
        (
            x.Id,
            x.EntityStatus,
            x.Title,
            x.Description,
            x.Price,
            x.PreviousPrice,
            priceConversion,
            status,
            CurrencyMapper.ToDto(x.Currency),
            category,
            UserShortMapper.ToDto(x.Owner),
            (x.Buyer is not null ? UserShortMapper.ToDto(x.Buyer) : null),
            x.Pictures
                .OrderBy(i => i.Order)
                .Select(AdvertisementImageMapper.ToDto)
                .ToList(),
            attributes,
            variants,
            x.SoldAt,
            x.CreatedAt
        );
    }
}
