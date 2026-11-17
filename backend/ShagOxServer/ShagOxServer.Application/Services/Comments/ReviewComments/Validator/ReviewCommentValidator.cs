using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewComments;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Domain.Entities.Comments.Review;

namespace ShagOxServer.Application.Services.Comments.ReviewComments.Validator;
public class ReviewCommentValidator
    : BaseValidator<ReviewComment>
{
    public ReviewCommentValidator(
        IRepository<ReviewComment> repository,
        IQueryRepository<ReviewComment> commentRepository,
        IReviewCommentExistsRepository commentExistsRepository,
        IDataCacheService cache
    ) 
        : base(repository, commentRepository, commentExistsRepository, cache)
    {
    }
}
