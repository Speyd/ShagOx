using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Review.ReviewRepliImages;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;
using ShagOxServer.Domain.Filters.Specification.Pictures.Review;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Core.Multiple.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Review.ReviewRepliImages.Query;
public partial class ReviewRepliImageQueryRepository
    : PictureOrderQueryRepository<ReviewRepliImage,
        ReviewRepliImageSearchFilter>,
      IReviewRepliImageQueryRepository
{
    public ReviewRepliImageQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }
}
