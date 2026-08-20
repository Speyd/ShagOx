using ShagOxServer.Application.DTOs.Specification.Pictures.Images;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Specification.Pictures.Images.Mapping;
public static class ImageMapper
{
    public static ImageDto ToDto(
        Image image)
    {
        return new ImageDto(
            image.Id,
            image.Url,
            image.PublicId,
            image.Order,
            image.AdvertisementId
        );
    }
}