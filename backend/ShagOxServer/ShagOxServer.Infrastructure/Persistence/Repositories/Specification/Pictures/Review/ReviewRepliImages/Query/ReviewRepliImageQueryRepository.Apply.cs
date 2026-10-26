using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Review.ReviewRepliImages;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;
using ShagOxServer.Domain.Filters.Specification.Pictures.Review;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Core.Multiple.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Review.ReviewRepliImages.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Review.ReviewRepliImages.Query;
public partial class ReviewRepliImageQueryRepository
    : PictureOrderQueryRepository<ReviewRepliImage, 
        ReviewRepliImageSearchFilter>,
      IReviewRepliImageQueryRepository
{
    protected override IQueryable<ReviewRepliImage> ApplyIncludes(
        IQueryable<ReviewRepliImage> query)
    {
        return query.WithIncludes();
    }

    public override IQueryable<ReviewRepliImage> ApplyFilter(
        IQueryable<ReviewRepliImage> query,
        ReviewRepliImageSearchFilter filter)
    {
        return query.Filter(filter);
    }
}
