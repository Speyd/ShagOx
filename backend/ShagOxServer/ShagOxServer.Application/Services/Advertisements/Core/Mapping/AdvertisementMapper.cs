using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.DTOs.Advertisements.Core;
using ShagOxServer.Application.DTOs.Dictionaries.Categories;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping;
using ShagOxServer.Application.Services.Auth.Users.Core.Mapping;
using ShagOxServer.Application.Services.Dictionaries.Categories.Mapping;
using ShagOxServer.Application.Services.Specification.Currencies.Mapping;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Mapping;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Core.Mapping;
public static class AdvertisementMapper
{
    public static AdvertisementDto ToDto(
        Advertisement x,
        Dictionary<long, List<VariantAttributeDto>> variants,
        CategoryDto category)
    {
        return new AdvertisementDto
        (
            x.Id,
            x.Title,
            x.Description,
            CurrencyMapper.ToDto(x.Currency),
            category,
            UserShortMapper.ToDto(x.Seller),
            (x.Buyer is not null ? UserShortMapper.ToDto(x.Buyer) : null),
            x.Images
                .OrderBy(i => i.Order)
                .Select(ImageMapper.ToDto)
                .ToList(),
            x.Attributes,
            VariantToDto(x, variants),
            x.SoldAt,
            x.CreatedAt
        );
    }

    internal static List<AdvertisementVariantDto> VariantToDto(
        Advertisement x, 
        Dictionary<long, List<VariantAttributeDto>> variants)
    {
        return x.Variants.Select(variant =>
        {
            variants.TryGetValue(
                variant.Id,
                out var attributes);

            return AdvertisementVariantMapper.ToDto(
                variant,
                attributes ?? []);
        }).ToList();
    }
}