namespace ShagOxServer.Domain.Filters.Comments;
public record CommentSearchFilter
(
    long? CommentableEntityId,
    long? OwnerId,
    string? Text,
    DateTime? CreatedAt,
    DateTime? UpdatedAt
) : BaseFilter();
