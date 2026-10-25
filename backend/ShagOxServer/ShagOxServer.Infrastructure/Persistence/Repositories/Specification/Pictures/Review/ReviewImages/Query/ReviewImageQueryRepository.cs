using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Review.ReviewImages;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;
using ShagOxServer.Domain.Filters.Specification.Pictures.Review;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Core.Multiple.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Review.ReviewImages;
public partial class ReviewImageQueryRepository
    : PictureOrderQueryRepository<ReviewImage, ReviewImageSearchFilter>,
      IReviewImageQueryRepository
{
    public ReviewImageQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }
}
