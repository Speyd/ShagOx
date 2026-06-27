using ShagOxServer.Application.DTOs.Advertisements;
using ShagOxServer.Domain.Entities;

namespace ShagOxServer.Application.Services.Advertisements.Mapping;
public static class AdvertisementMapper
{
    public static UserDto ToDto(Advertisement x)
    {
        return new UserDto
        (
            x.Id,
            x.Title,
            x.Description,
            x.Price,
            x.PreviousPrice,
            x.CurrencyId,
            x.Currency?.Code ?? "",
            x.CategoryId,
            x.Category?.Name ?? "",
            x.SellerId,
            x.BuyerId,
            x.Images?
                .OrderBy(i => i.Order)
                .Select(i => i.Url)
                .ToList()
                ?? new List<string>(),
            x.Properties,
            x.SoldAt,
            x.CreatedAt
        );
    }
}