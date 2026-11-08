using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Application.DTOs.Comments.Core.Query;
using ShagOxServer.Application.DTOs.Specification.Pictures.Review.ReviewImages.Query;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Comments.ReviewComments.Query;
public sealed record ReviewCommentDto
(
    long Id,
    EntityStatus EntityStatus,
    UserCommentDto Owner,
    long CommentableEntityId,
    string Text,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    int Grade,
    int RepliesCount,
    List<ReviewImageDto> Pictures
) : CommentDto(Id,
    EntityStatus,
    Owner, 
    CommentableEntityId,
    Text, 
    CreatedAt, 
    UpdatedAt
);
