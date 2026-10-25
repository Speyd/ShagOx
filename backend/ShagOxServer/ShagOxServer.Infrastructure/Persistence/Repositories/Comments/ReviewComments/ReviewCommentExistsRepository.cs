using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewComments;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Comments.ReviewComments;
public class ReviewCommentExistsRepository
    : ExistsOwnerRepository<ReviewComment>,
      IReviewCommentExistsRepository
{
    public ReviewCommentExistsRepository(
        AppDbContext db)
        : base(db)
    { }
}
