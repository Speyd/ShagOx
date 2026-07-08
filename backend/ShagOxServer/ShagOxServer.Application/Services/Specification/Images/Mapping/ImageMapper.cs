using ShagOxServer.Application.DTOs.Specification.Images;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Services.Specification.Images.Mapping;
public static class ImageMapper
{
    public static ImageDto ToDto(Image image)
    {
        return new ImageDto(
            image.Id,
            image.Url,
            image.Order,
            image.PublicId,
            image.AdvertisementId
        );
    }
}
