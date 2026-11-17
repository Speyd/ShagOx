using ShagOxServer.Application.DTOs.Comments.ReviewRepliComments.Update;
using ShagOxServer.Domain.Entities.Comments.Review;

namespace ShagOxServer.Application.Services.Comments.ReviewRepliComments.Update;
public static class ReviewRepliCommentUpdater
{
    public static int ApplyUpdates(
        ReviewRepliComment comment,
        ReviewRepliCommentUpdateRequest request)
    {
        int countUpdated = 0;

        var text = request.Text?.Trim();

        if (!string.IsNullOrEmpty(text) &&
            text != comment.Text)
        {
            comment.Text = text;

            countUpdated++;
        }

        if (request.Pictures is { Count: > 0 })
            countUpdated++;

        if (countUpdated > 0)
            comment.UpdatedAt = DateTime.UtcNow;

        return countUpdated;
    }
}
