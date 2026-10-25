using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Application.DTOs.Comments.ReviewRepliComments.Query;
using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Application.Services.Comments.ReviewRepliComments.Mapping;
public static class ReviewRepliCommentMapper
{
    public static ReviewRepliCommentDto ToDto(
        ReviewRepliComment comment,
        UserCommentDto user)
    {
        return new ReviewRepliCommentDto(
            comment.Id,
            user,
            comment.CommentableEntityId,
            comment.Text,
            comment.CreatedAt,
            comment.UpdatedAt
            //comment.Pictures
            //    .OrderBy(i => i.Order)
            //    .Select(ReviewImageMapper.ToDto)
            //    .ToList()
        );
    }
}
