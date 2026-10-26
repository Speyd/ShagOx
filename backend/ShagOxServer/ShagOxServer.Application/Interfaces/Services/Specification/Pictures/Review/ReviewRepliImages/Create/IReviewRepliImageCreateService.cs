using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core.Create;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewRepliImages.Create;
public interface IReviewRepliImageCreateService
    : IPictureOrderCreateService<ReviewRepliImage,
        PictureOrderCreateRequest>
{
}
