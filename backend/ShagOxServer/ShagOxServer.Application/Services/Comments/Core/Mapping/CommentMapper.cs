using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Query;
using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Application.Services.Comments.Core.Mapping;
public static class CommentMapper
{
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
