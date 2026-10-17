using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewComments.Update;
public static class ReviewImageUpdater
{
    public static int ApplyUpdates(
        ReviewImage image,
        PictureOrderUpdateRequest request)
    {
        var updated = 0;

        if (request.Order.HasValue)
        {
            image.Order = request.Order.Value;
            updated++;
        }

        if (request.OwnerId is not null &&
            image.OwnerId != request.OwnerId.Value)
        {
            image.OwnerId = request.OwnerId.Value;
            updated++;
        }

        return updated;
    }
}
