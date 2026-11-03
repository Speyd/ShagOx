using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Comments.Core.Query;
public record CommentDto
(
    long Id,
    EntityStatus EntityStatus,
    UserCommentDto Owner,
    long CommentableEntityId,
    string Text,
    DateTime CreatedAt,
    DateTime? UpdatedAt
): BaseDto(Id, EntityStatus);
