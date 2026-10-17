using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Specification.Pictures.Images.Mapping;
public static class AdvertisementImageCacheMapper
{
    public static PictureCacheInfo ToInfo(
        AdvertisementImage image)
    {
        return new PictureCacheInfo(
            image.Id,
            image.OwnerId
        );
    }
}
