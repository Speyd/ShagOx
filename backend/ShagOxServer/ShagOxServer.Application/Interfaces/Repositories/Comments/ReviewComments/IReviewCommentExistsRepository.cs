using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Special;
using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewComments;
public interface IReviewCommentExistsRepository
    : IExistsRepository<ReviewComment>,
    IExistsOwnerRepository
{
}
