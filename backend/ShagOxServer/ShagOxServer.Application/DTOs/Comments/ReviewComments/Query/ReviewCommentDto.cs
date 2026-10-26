using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Application.DTOs.Comments.Core.Query;
using ShagOxServer.Application.DTOs.Specification.Pictures.Review.ReviewImages.Query;

namespace ShagOxServer.Application.DTOs.Comments.ReviewComments.Query;
public sealed record ReviewCommentDto
(
    long Id,
    UserCommentDto Owner,
    long CommentableEntityId,
    string Text,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    int Grade,
    List<ReviewImageDto> Pictures
) : CommentDto(Id,
    Owner, 
    CommentableEntityId,
    Text, 
    CreatedAt, 
    UpdatedAt
);
