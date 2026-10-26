using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewRepliComments;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Comments.ReviewRepliComments;
public class ReviewRepliCommentExistsRepository
    : ExistsOwnerRepository<ReviewRepliComment>,
      IReviewRepliCommentExistsRepository
{
    public ReviewRepliCommentExistsRepository(
        AppDbContext db)
        : base(db)
    { }
}
