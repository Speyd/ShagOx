using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Application.DTOs.Comments.Core.Cache;
using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Query;
using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Application.Services.Comments.Core.Mapping;
public static class CommentCacheMapper
{
    public static CommentCacheInfo ToInfo(
        Comment comment)
    {
        return new CommentCacheInfo(
            comment.Id,
            comment.CommentableEntityId
        );
    }

    public static UserCommentDto ToUserCommentDto(
        Comment comment,
        AvatarDto? avatar)
    {
        return new UserCommentDto(
            comment.Owner.Id,
            comment.Owner.FirstName,
            comment.Owner.LastName,
            comment.Owner.UserName,
            avatar,
            comment.Owner.CityId,
            comment.Owner.LastSeenAt);
    }
}
