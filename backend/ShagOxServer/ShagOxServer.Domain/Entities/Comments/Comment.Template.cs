using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Comments;
public class Comment<TCommentable>
    : Comment
    where TCommentable : BaseEntity
{
    public TCommentable CommentableEntity { get; set; } = null!;


    public override string ToString()
    {
        return $"CommentableId: {CommentableEntityId}";
    }
}
