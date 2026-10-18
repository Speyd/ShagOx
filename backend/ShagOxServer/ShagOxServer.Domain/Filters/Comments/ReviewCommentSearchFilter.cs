namespace ShagOxServer.Domain.Filters.Comments;
public sealed record ReviewCommentSearchFilter
(
    long? CommentableEntityId,
    long? OwnerId,
    string? Text,
    DateTime? CreatedAt,
    DateTime? UpdatedAt,
    int? Grade
) : BaseFilter();
