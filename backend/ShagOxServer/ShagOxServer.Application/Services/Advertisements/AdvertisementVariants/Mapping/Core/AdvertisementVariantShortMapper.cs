using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query.Core;
using ShagOxServer.Application.DTOs.Common.Currencies;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping;
public static class AdvertisementVariantShortMapper
{
    public static AdvertisementVariantShortDto ToDto(
        AdvertisementVariant x, 
        ConvertedPricesDto priceConversion)
    {
        return new AdvertisementVariantShortDto
        (
            x.Id,
            x.EntityStatus,
            x.AdvertisementId,
            x.Price,
            x.PreviousPrice,
            priceConversion,
            x.Stock,
            x.Attributes
        );
    }

    public static List<AdvertisementVariantShortDto> ToVariantsShortDto(
        ConvertedPricesDto priceConversion,
        ICollection<AdvertisementVariant> variants)
    {
        var result = new List<AdvertisementVariantShortDto>();

        foreach (var x in variants)
        {
            result.Add(ToVariantShortDto(priceConversion, x));
        }

        return result;
    }

    public static AdvertisementVariantShortDto ToVariantShortDto(
        ConvertedPricesDto priceConversion,
        AdvertisementVariant variant)
    {
        var converts = new ConvertedPricesDto(
            priceConversion.Price with
            {
                ConvertPrice = variant.Price *
                    priceConversion.Price.Coefficient
            },
            priceConversion.PreviousPrice with
            {
                ConvertPrice = variant.PreviousPrice *
                priceConversion.PreviousPrice.Coefficient
            }
        );

        return AdvertisementVariantShortMapper
                .ToDto(variant, converts);
    }
}
