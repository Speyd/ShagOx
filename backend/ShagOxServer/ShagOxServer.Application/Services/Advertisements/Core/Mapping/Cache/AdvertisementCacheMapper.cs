using ShagOxServer.Application.DTOs.Advertisements.Core.Cache;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping.Cache;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Core.Mapping.Cache;
public static class AdvertisementCacheMapper
{
    public static AdvertisementCacheInfo ToInfo(
        Advertisement x,
        EntityStatus status = EntityStatus.Active)
    {
        return new AdvertisementCacheInfo
        (
            x.Id,
            x.OwnerId,
            x.BuyerId,
            x.Variants
                .Select(x => AdvertisementVariantCacheMapper
                    .ToInfo(x, status))
                .ToList(),
            status
        );
    }
}
