using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewComments.Validator;
public class ReviewImageValidator
    : BaseValidator<ReviewImage>
{
    public ReviewImageValidator(
        IRepository<ReviewImage> repository,
        IQueryRepository<ReviewImage> imageRepository,
        IExistsRepository<ReviewImage> imageExistsRepository,
        IDataCacheService cache
    ) 
        : base(repository, imageRepository, imageExistsRepository, cache)
    {
    }
}
