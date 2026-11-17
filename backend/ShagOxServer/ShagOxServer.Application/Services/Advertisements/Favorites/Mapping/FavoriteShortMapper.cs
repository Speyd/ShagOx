using ShagOxServer.Application.DTOs.Advertisements.Favorites.Query;
using ShagOxServer.Application.DTOs.Advertisements.Statuses.Query;
using ShagOxServer.Application.DTOs.Common.Currencies;
using ShagOxServer.Application.Services.Advertisements.Core.Mapping.Core;
using ShagOxServer.Application.Services.Auth.Users.Core.Mapping;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Mapping;
public static class FavoriteShortMapper
{
    public static FavoriteShortDto ToDto(
        Favorite x,
        ConvertedPricesDto priceConversion,
        StatusDto statusDto)
    {
        return new FavoriteShortDto
        (
            x.Id,
            x.EntityStatus,
            UserShortMapper.ToDto(x.Owner),
            AdvertisementShortMapper.ToDto(
                x.Advertisement,
                priceConversion,
                statusDto)
        );
    }
}
