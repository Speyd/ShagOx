using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewComments;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Application.Services.Comments.ReviewComments.Validator;
public class ReviewCommentValidator
    : BaseValidator<ReviewComment>
{
    public ReviewCommentValidator(
        IQueryRepository<ReviewComment> commentRepository,
        IReviewCommentExistsRepository commentExistsRepository,
        IDataCacheService cache
    ) 
        : base(commentRepository, commentExistsRepository, cache)
    {
    }
}
