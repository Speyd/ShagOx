using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewRepliImages.Validator;
public class ReviewRepliImageValidator
    : BaseValidator<ReviewRepliImage>
{
    public ReviewRepliImageValidator(
        IQueryRepository<ReviewRepliImage> imageRepository,
        IExistsRepository<ReviewRepliImage> imageExistsRepository,
        IDataCacheService cache
    )
        : base(imageRepository, imageExistsRepository, cache)
    {
    }
}
