using ShagOxServer.Application.DTOs.Advertisements;
using ShagOxServer.Application.Services.Auth.Users.Mapping;
using ShagOxServer.Application.Services.Dictionaries.Categories.Mapping;
using ShagOxServer.Application.Services.Specification.Currencies.Mapping;
using ShagOxServer.Application.Services.Specification.Images.Mapping;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Mapping;
public static class AdvertisementMapper
{
    public static AdvertisementDto ToDto(Advertisement x)
    {
        return new AdvertisementDto
        (
            x.Id,
            x.Title,
            x.Description,
            x.Price,
            x.PreviousPrice,
            CurrencyMapper.ToDto(x.Currency),
            CategoryMapper.ToDto(x.Category),
            UserShortMapper.ToDto(x.Seller),
            (x.Buyer is not null ? UserShortMapper.ToDto(x.Buyer) : null),
            x.Images
                .OrderBy(i => i.Order)
                .Select(ImageMapper.ToDto)
                .ToList(),
            x.Properties,
            x.SoldAt,
            x.CreatedAt
        );
    }
}