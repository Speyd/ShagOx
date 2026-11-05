using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.DTOs.Advertisements.Core.Query;
using ShagOxServer.Application.DTOs.Common.Currencies;
using ShagOxServer.Application.DTOs.Specification.Currencies.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Currencies;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Mapping;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Core.Mapping;
public static class AdvertisementShortMapper
{
    public static AdvertisementShortDto ToDto(
        Advertisement x,
        ConvertedPricesDto priceConversion)
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
            x.Pictures
                .OrderBy(i => i.Order)
                .Select(AdvertisementImageMapper.ToDto)
                .FirstOrDefault(),
            x.Attributes,
            ToVariantShortDto(priceConversion, x.Variants),
            x.SoldAt,
            x.CreatedAt
        );
    }

    public static List<AdvertisementVariantShortDto> ToVariantShortDto(
        ConvertedPricesDto priceConversion,
        ICollection<AdvertisementVariant> variants)
    {
        var result = new List<AdvertisementVariantShortDto>();

        foreach(var x in variants)
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
