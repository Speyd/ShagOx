using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query.Catalog;
using ShagOxServer.Application.DTOs.Advertisements.Core.Query.Core;
using ShagOxServer.Application.DTOs.Common.Currencies;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Query;
using ShagOxServer.Application.Services.Base.Localized;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Core.Query;

public partial class AdvertisementQueryService
    : BaseLocalizedQueryService<
        AdvertisementDto,
        Advertisement,
        AdvertisementSearchFilter>,
      IAdvertisementQueryService
{
    public async Task<ConvertedPricesDto> ConvertPricesAsync(
        Advertisement advert,
        CancellationToken cancellationToken = default)
    {
        var price = advert.Price ??
            advert.Variants.FirstOrDefault()?.Price;

        var previousPrice = advert.PreviousPrice ??
            advert.Variants.FirstOrDefault()?.PreviousPrice;

        return await _priceConversion.ConvertPricesAsync(
            price,
            previousPrice,
            advert.Currency);
    }

    public async Task<List<AdvertisementVariantCatalogDto>> LoadVariantDtosAsync(
        Advertisement advert,
        CancellationToken cancellationToken = default)
    {
        var variants = new List<AdvertisementVariantCatalogDto>();

        foreach (var variant in advert.Variants)
        {
            var variantDto = await _variantService
                .ApplyCatalogMapperAsync(variant);

            variants.Add(variantDto);
        }

        return variants;
    }
}
