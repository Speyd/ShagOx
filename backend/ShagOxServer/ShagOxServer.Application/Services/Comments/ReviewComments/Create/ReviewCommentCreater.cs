using ShagOxServer.Application.DTOs.Comments.ReviewComments.Create;
using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Application.Services.Comments.ReviewComments.Create;
public static class ReviewCommentCreater
{
    public static ReviewComment Create(
        long userId,
        ReviewCommentCreateRequest request)
    {
        return new ReviewComment
        {
            OwnerId = userId,
            CommentableEntityId = request.CommentableEntityId,
            Text = request.Text,
            CreatedAt = DateTime.UtcNow
        };
    }
}
