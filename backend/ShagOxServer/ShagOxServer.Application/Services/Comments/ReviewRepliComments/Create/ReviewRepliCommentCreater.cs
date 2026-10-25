using ShagOxServer.Application.DTOs.Comments.ReviewRepliComments.Create;
using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Application.Services.Comments.ReviewRepliComments.Create;
public static class ReviewRepliCommentCreater
{
    public static ReviewRepliComment Create(
        long userId,
        ReviewRepliCommentCreateRequest request)
    {
        return new ReviewRepliComment
        {
            OwnerId = userId,
            CommentableEntityId = request.CommentableEntityId,
            Text = request.Text,
            CreatedAt = DateTime.UtcNow
        };
    }
}
