using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewComments;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Comments.ReviewComments;
public class ReviewCommentExistsRepository
    : ExistsRepository<ReviewComment>,
      IReviewCommentExistsRepository
{
    public ReviewCommentExistsRepository(
        AppDbContext db)
        : base(db)
    { }

    public async Task<bool> ExistsAsync(
        long advertisementVariantId,
        long basketId,
        CancellationToken cancellationToken = default)
    {
        return await _db.BasketItems
            .AnyAsync(c =>
                c.AdvertisementVariantId == advertisementVariantId &&
                c.BasketId == basketId
            );
    }

    public async Task<bool> IsOwnerAsync(
        long entityId,
        long userId,
        CancellationToken cancellationToken = default)
    {
        return await _db.ReviewComments
           .AnyAsync(c =>
               c.Id == entityId &&
               c.OwnerId == userId
           );
    }
}
