using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;

namespace ShagOxServer.Application.DTOs.Comments.ReviewComments.Query;
public sealed record ReviewCommentDto
(
    long Id,
    UserCommentDto Owner,
    long CommentableEntityId,
    string Text,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    int Grade
) : CommentDto(Id,
    Owner, 
    CommentableEntityId,
    Text, 
    CreatedAt, 
    UpdatedAt
);
