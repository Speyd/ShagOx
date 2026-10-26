using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewRepliComments;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Application.Services.Comments.ReviewRepliComments.Validator;
public class ReviewRepliCommentValidator
    : BaseValidator<ReviewRepliComment>
{
    public ReviewRepliCommentValidator(
        IQueryRepository<ReviewRepliComment> commentRepository,
        IReviewRepliCommentExistsRepository commentExistsRepository,
        IDataCacheService cache
    )
        : base(commentRepository, commentExistsRepository, cache)
    {
    }
}
