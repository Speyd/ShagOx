using ShagOxServer.Application.DTOs.Advertisements.Core.Query.Catalog;
using ShagOxServer.Application.DTOs.Advertisements.Statuses.Query;
using ShagOxServer.Application.DTOs.Common.Currencies;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Mapping;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Core.Mapping;
public static class AdvertisementShortMapper
{
    public static AdvertisementShortDto ToDto(
        Advertisement x,
        ConvertedPricesDto priceConversion,
        StatusDto statusDto)
    {
        return new AdvertisementShortDto
        (
            x.Id,
            x.EntityStatus,
            x.Title,
            x.Description,
            x.Price,
            x.PreviousPrice,
            priceConversion,
            x.CurrencyId,
            x.CategoryId,
            x.OwnerId,
            x.BuyerId,
            statusDto,
            x.Pictures
                .OrderBy(i => i.Order)
                .Select(AdvertisementImageMapper.ToDto)
                .FirstOrDefault(),
            x.Attributes,
            AdvertisementVariantShortMapper
                .ToVariantShortDto(priceConversion, x.Variants),
            x.SoldAt,
            x.CreatedAt
        );
    }
}
