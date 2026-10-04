using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.DTOs.Advertisements.Core.Query;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Core.Mapping;
public static class AdvertisementShortMapper
{
    public static AdvertisementShortDto ToDto(
        Advertisement x)
    {
        return new AdvertisementShortDto
        (
            x.Id,
            x.Title,
            x.Description,
            x.CurrencyId,
            x.CategoryId,
            x.Seller.Id,
            x.Buyer?.Id,
            x.Images
                .OrderBy(i => i.Order)
                .Select(i => i.Id)
                .ToList(),
            x.Attributes,
            x.Variants
                .Select(AdvertisementVariantShortMapper.ToDto)
                .ToList(),
            x.SoldAt,
            x.CreatedAt
        );
    }
}