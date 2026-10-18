using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewComments.Validator;
public class ReviewImageValidator
    : BaseValidator<ReviewImage>
{
    public ReviewImageValidator(
        IQueryRepository<ReviewImage> imageRepository,
        IExistsRepository<ReviewImage> imageExistsRepository
    ) : base(imageRepository, imageExistsRepository)
    {
    }
}
