using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
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

    public static List<AdvertisementVariantShortDto> ToVariantShortDto(
        ConvertedPricesDto priceConversion,
        ICollection<AdvertisementVariant> variants)
    {
        var result = new List<AdvertisementVariantShortDto>();

        foreach (var x in variants)
        {
            var converts = new ConvertedPricesDto(
                priceConversion.Price with
                {
                    ConvertPrice = x.Price *
                        priceConversion.Price.Coefficient
                },
                priceConversion.PreviousPrice with
                {
                    ConvertPrice = x.PreviousPrice *
                        priceConversion.PreviousPrice.Coefficient
                }
            );

            result.Add(AdvertisementVariantShortMapper
                .ToDto(x, converts));
        }

        return result;
    }
}
