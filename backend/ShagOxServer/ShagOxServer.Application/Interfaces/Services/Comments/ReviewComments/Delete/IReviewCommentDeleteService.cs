using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Comments.Review;

namespace ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Delete;
public interface IReviewCommentDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<ReviewComment, DeleteResponse>
{
}
