using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewRepliComments;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Application.Services.Comments.ReviewRepliComments.Validator;
public class ReviewRepliCommentValidator
    : BaseValidator<ReviewRepliComment>
{
    public ReviewRepliCommentValidator(
        IRepository<ReviewRepliComment> repository,
        IQueryRepository<ReviewRepliComment> commentRepository,
        IReviewRepliCommentExistsRepository commentExistsRepository,
        IDataCacheService cache
    )
        : base(repository, commentRepository, commentExistsRepository, cache)
    {
    }
}
