using ShagOxServer.Application.DTOs.Specification.Pictures.Review.ReviewRepliImages.Query;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewRepliImages.Mapping;
public static class ReviewRepliImageMapper
{
    public static ReviewRepliImageDto ToDto(
        ReviewRepliImage image)
    {
        return new ReviewRepliImageDto(
            image.Id,
            image.Url,
            image.PublicId,
            image.Order,
            image.OwnerId
        );
    }
}
