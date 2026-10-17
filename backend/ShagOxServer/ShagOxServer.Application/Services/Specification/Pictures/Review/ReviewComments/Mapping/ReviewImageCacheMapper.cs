using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewComments.Mapping;
public static class ReviewImageCacheMapper
{
    public static PictureCacheInfo ToInfo(
        ReviewImage image)
    {
        return new PictureCacheInfo(
            image.Id,
            image.OwnerId
        );
    }
}
