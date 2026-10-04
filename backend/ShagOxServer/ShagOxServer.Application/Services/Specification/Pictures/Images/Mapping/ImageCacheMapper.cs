using ShagOxServer.Application.DTOs.Specification.Pictures.Images.Cache;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Specification.Pictures.Images.Mapping;
public static class ImageCacheMapper
{
    public static ImageCacheInfo ToInfo(
        Image image)
    {
        return new ImageCacheInfo(
            image.Id,
            image.AdvertisementId
        );
    }
}