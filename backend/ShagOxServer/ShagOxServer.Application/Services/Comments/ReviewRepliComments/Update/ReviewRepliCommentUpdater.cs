using ShagOxServer.Application.DTOs.Comments.ReviewRepliComments.Update;
using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Application.Services.Comments.ReviewRepliComments.Update;
public static class ReviewRepliCommentUpdater
{
    public static int ApplyUpdates(
        ReviewRepliComment comment,
        ReviewRepliCommentUpdateRequest request)
    {
        int countUpdated = 0;

        if (!string.IsNullOrWhiteSpace(request.Text))
        {
            comment.Text = request.Text;

            countUpdated++;
        }

        return countUpdated;
    }
}
