using ShagOxServer.Application.DTOs.Advertisements.Core.Query;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Mapping;
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
            x.EntityStatus,
            x.Title,
            x.Description,
            x.CurrencyId,
            x.CategoryId,
            x.OwnerId,
            x.BuyerId,
            x.Pictures
                .OrderBy(i => i.Order)
                .Select(AdvertisementImageMapper.ToDto)
                .FirstOrDefault(),
            x.Attributes,
            x.Variants
                .Select(AdvertisementVariantShortMapper.ToDto)
                .ToList(),
            x.SoldAt,
            x.CreatedAt
        );
    }
}
