using ShagOxServer.Application.DTOs.Specification.Pictures.AdvertisementImages.Query;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Specification.Pictures.Images.Mapping;
public static class AdvertisementImageMapper
{
    public static AdvertisementImageDto ToDto(
        AdvertisementImage image)
    {
        return new AdvertisementImageDto(
            image.Id,
            image.Url,
            image.PublicId,
            image.Order,
            image.OwnerId
        );
    }
}
