using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Update;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewImages.Update;
public interface IReviewImageUpdateService
    : IPictureUpdateService<ReviewImage,
        PictureOrderUpdateRequest>
{
}
