using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Application.DTOs.Comments.Core.Query;
using ShagOxServer.Application.DTOs.Specification.Pictures.Review.ReviewRepliImages.Query;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Comments.ReviewRepliComments.Query;
public sealed record ReviewRepliCommentDto
(
    long Id,
    EntityStatus EntityStatus,
    UserCommentDto Owner,
    long CommentableEntityId,
    string Text,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    long? ParentReplyId,
    List<long> RepliComments,
    List<ReviewRepliImageDto> Pictures
) : CommentDto(Id,
    EntityStatus,
    Owner,
    CommentableEntityId,
    Text,
    CreatedAt,
    UpdatedAt
);
