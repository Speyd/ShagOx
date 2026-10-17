using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Comments;
public class Comment<TCommentable>
    : BaseEntity
    where TCommentable : BaseEntity
{
    public string Text { get; set; } = "";

    public long CommentableEntityId { get; set; }
    public TCommentable CommentableEntity { get; set; } = null!;


    public override string ToString()
    {
        return $"CommentableId: {CommentableEntityId}";
    }
}
