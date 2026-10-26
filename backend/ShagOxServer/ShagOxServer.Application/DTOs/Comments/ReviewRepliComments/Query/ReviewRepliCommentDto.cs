using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Application.DTOs.Comments.Core.Query;

namespace ShagOxServer.Application.DTOs.Comments.ReviewRepliComments.Query;
public sealed record ReviewRepliCommentDto
(
    long Id,
    UserCommentDto Owner,
    long CommentableEntityId,
    string Text,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    List<long> RepliComments
//List<ReviewRepliImageDto> Pictures
) : CommentDto(Id,
    Owner,
    CommentableEntityId,
    Text,
    CreatedAt,
    UpdatedAt
);
