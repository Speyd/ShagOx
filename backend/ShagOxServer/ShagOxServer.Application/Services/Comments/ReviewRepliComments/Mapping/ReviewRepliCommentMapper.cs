using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Application.DTOs.Comments.ReviewRepliComments.Query;
using ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewRepliImages.Mapping;
using ShagOxServer.Domain.Entities.Comments.Review;

namespace ShagOxServer.Application.Services.Comments.ReviewRepliComments.Mapping;
public static class ReviewRepliCommentMapper
{
    public static ReviewRepliCommentDto ToDto(
        ReviewRepliComment comment,
        UserCommentDto user)
    {
        return new ReviewRepliCommentDto(
            comment.Id,
            comment.EntityStatus,
            user,
            comment.CommentableEntityId,
            comment.Text,
            comment.CreatedAt,
            comment.UpdatedAt,
            comment.Replies
                .Select(x => x.Id)
                .ToList(),
            comment.Pictures
                .OrderBy(i => i.Order)
                .Select(ReviewRepliImageMapper.ToDto)
                .ToList()
        );
    }
}
