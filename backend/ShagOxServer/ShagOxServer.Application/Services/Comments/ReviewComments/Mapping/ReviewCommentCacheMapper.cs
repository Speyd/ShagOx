using ShagOxServer.Application.DTOs.Comments.ReviewComments.Cache;
using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Application.Services.Comments.ReviewComments.Mapping;
public static class ReviewCommentCacheMapper
{
    public static ReviewCommentCacheInfo ToInfo(
        ReviewComment comment)
    {
        return new ReviewCommentCacheInfo(
            comment.Id,
            comment.CommentableEntityId
        );
    }
}
