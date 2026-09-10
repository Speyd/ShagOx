using ShagOxServer.Application.DTOs.Advertisements;
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
            x.Stock,
            x.Price,
            x.PreviousPrice,
            x.CurrencyId,
            x.CategoryId,
            x.Seller.Id,
            x.Buyer?.Id,
            x.Images
                .OrderBy(i => i.Order)
                .Select(i => i.Id)
                .ToList(),
            x.Properties,
            x.SoldAt,
            x.CreatedAt
        );
    }
}