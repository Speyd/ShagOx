using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Domain.Entities.Comments;
public class Comment<TCommentable>
    : BaseEntity
    where TCommentable : BaseEntity
{
    public string Text { get; set; } = "";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public long OwnerId { get; set; }
    public User Owner { get; set; } = null!;

    public long CommentableEntityId { get; set; }
    public TCommentable CommentableEntity { get; set; } = null!;


    public override string ToString()
    {
        return $"CommentableId: {CommentableEntityId}";
    }
}
