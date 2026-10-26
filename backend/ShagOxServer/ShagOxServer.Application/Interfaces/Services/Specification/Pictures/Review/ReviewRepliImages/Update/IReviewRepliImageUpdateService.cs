using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Request;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Update;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewRepliImages.Update;
public interface IReviewRepliImageUpdateService
    : IPictureUpdateService<ReviewRepliImage,
        PictureOrderUpdateRequest>
{
}
