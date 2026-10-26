using ShagOxServer.Domain.Base.Owner;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Domain.Entities.Comments;
public abstract class Comment
    : OwnedEntity<User>
{
    public string Text { get; set; } = "";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public long CommentableEntityId { get; set; }


    public override string ToString()
    {
        return $"OwnerId: {OwnerId}";
    }
}
