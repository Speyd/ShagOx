using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Cache;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping.Cache;
public static class AdvertisementVariantCacheMapper
{
    public static AdvertisementVariantCacheInfo ToInfo(
        AdvertisementVariant x,
        EntityStatus status = EntityStatus.Active)
    {
        return new AdvertisementVariantCacheInfo
        (
            x.Id,
            status
        );
    }
}
