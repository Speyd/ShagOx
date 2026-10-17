using ShagOxServer.Application.DTOs.Comments.ReviewComments.Update;
using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Application.Services.Comments.ReviewComments.Update;
public static class ReviewCommentUpdater
{
    public static int ApplyUpdates(
        ReviewComment comment,
        ReviewCommentUpdateRequest request)
    {
        int countUpdated = 0;

        if (!string.IsNullOrWhiteSpace(request.Text))
        {
            comment.Text = request.Text;

            countUpdated++;
        }

        if (request.Grade.HasValue)
        {
            comment.Grade = request.Grade.Value;

            countUpdated++;
        }

        return countUpdated;
    }
}
