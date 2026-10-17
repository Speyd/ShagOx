using ShagOxServer.Application.DTOs.Advertisements.Core.Cache;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Core.Mapping;
public static class AdvertisementCacheMapper
{
    public static AdvertisementCacheInfo ToInfo(
        Advertisement x)
    {
        return new AdvertisementCacheInfo
        (
            x.Id,
            x.SellerId,
            x.BuyerId,
            x.Variants
                .Select(x => x.Id)
                .ToList()
        );
    }
}
