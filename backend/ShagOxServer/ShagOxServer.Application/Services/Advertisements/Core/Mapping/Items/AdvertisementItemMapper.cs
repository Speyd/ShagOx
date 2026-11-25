using ShagOxServer.Application.DTOs.Advertisements.Core.Query.LineItems;
using ShagOxServer.Application.DTOs.Advertisements.Statuses.Query;
using ShagOxServer.Application.DTOs.Common.Currencies;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Mapping;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Core.Mapping.Items;
public static class AdvertisementItemMapper
{
    public static AdvertisementLineItemDto ToDto(
        Advertisement x,
        ConvertedPricesDto priceConversion,
        StatusDto statusDto)
    {
        var variant = x.Variants.FirstOrDefault();

        return new AdvertisementLineItemDto
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
            variant is null ? null :
                AdvertisementVariantShortMapper
                    .ToVariantShortDto(priceConversion, variant),
            x.SoldAt,
            x.CreatedAt
        );
    }
}
