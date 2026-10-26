using ShagOxServer.Application.DTOs.Specification.Pictures.Review.ReviewImages.Query;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewComments.Mapping;
public static class ReviewImageMapper
{
    public static ReviewImageDto ToDto(
        ReviewImage image)
    {
        return new ReviewImageDto(
            image.Id,
            image.Url,
            image.PublicId,
            image.Order,
            image.OwnerId
        );
    }
}
