using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Delete;
using ShagOxServer.Domain.Entities.Comments.Review;

namespace ShagOxServer.Application.Interfaces.Services.Comments.ReviewRepliComments.Delete;
public interface IReviewRepliCommentDeleteService
    : IDeleteService<DeleteResponse>,
    ISoftDeleteService<ReviewRepliComment, DeleteResponse>
{
}
