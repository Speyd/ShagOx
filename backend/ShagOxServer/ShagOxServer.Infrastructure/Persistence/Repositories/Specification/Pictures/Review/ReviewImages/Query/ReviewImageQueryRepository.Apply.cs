using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Review.ReviewImages;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;
using ShagOxServer.Domain.Filters.Specification.Pictures.Review;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Core.Multiple.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Review.ReviewImages.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Review.ReviewImages;
public partial class ReviewImageQueryRepository
    : PictureOrderQueryRepository<ReviewImage, ReviewImageSearchFilter>,
      IReviewImageQueryRepository
{
    protected override IQueryable<ReviewImage> ApplyIncludes(
        IQueryable<ReviewImage> query)
    {
        return query.WithIncludes();
    }

    public override IQueryable<ReviewImage> ApplyFilter(
        IQueryable<ReviewImage> query,
        ReviewImageSearchFilter filter)
    {
        return query.Filter(filter);
    }
}
