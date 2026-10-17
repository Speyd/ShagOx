using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Review.ReviewImages;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;
using ShagOxServer.Domain.Filters.Specification.Pictures.Review;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Review.ReviewImages.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Review.ReviewImages;
public partial class ReviewImageQueryRepository
    : SearchRepository<ReviewImage, ReviewImageSearchFilter>,
      IReviewImageQueryRepository
{
    public ReviewImageQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<List<ReviewImage>> GetByOwnerAsync(
        long ownerId,
        CancellationToken cancellationToken = default)
    {
        return await _db.ReviewImages
          .WithIncludes()
          .Where(x => x.OwnerId == ownerId)
          .ToListAsync(cancellationToken);
    }
}
