using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Application.DTOs.Comments.Core.Cache;
using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Application.Services.Comments.Core.Mapping;
public static class CommentCacheMapper
{
    public static CommentCacheInfo ToInfo(
        Comment comment,
        EntityStatus status = EntityStatus.Active)
    {
        return new CommentCacheInfo(
            comment.Id,
            comment.CommentableEntityId,
            status
        );
    }

    public static UserCommentDto ToUserCommentDto(
        Comment comment,
        AvatarDto? avatar)
    {
        return new UserCommentDto(
            comment.Owner.Id,
            comment.Owner.EntityStatus,
            comment.Owner.FirstName,
            comment.Owner.LastName,
            comment.Owner.UserName,
            avatar,
            comment.Owner.CityId,
            comment.Owner.LastSeenAt);
    }
}
