using ShagOxServer.Application.DTOs.Comments.Core.Cache;
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
}
