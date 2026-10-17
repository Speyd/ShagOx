using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Application.DTOs.Comments.ReviewComments.Query;
using ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewComments.Mapping;
using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Application.Services.Comments.ReviewComments.Mapping;
public static class ReviewCommentMapper
{
    public static ReviewCommentDto ToDto(
        ReviewComment comment,
        UserCommentDto user)
    {
        return new ReviewCommentDto(
            comment.Id,
            user,
            comment.CommentableEntityId,
            comment.Text,
            comment.CreatedAt,
            comment.UpdatedAt,
            comment.Grade,
            comment.Pictures
                .OrderBy(i => i.Order)
                .Select(ReviewImageMapper.ToDto)
                .ToList()
        );
    }
}
