using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Application.DTOs.Base;

namespace ShagOxServer.Application.DTOs.Comments.Core.Query;
public record CommentDto
(
    long Id,
    UserCommentDto Owner,
    long CommentableEntityId,
    string Text,
    DateTime CreatedAt,
    DateTime? UpdatedAt
): BaseDto(Id);
