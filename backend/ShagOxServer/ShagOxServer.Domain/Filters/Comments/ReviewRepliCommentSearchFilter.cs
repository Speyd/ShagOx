namespace ShagOxServer.Domain.Filters.Comments;
public sealed record ReviewRepliCommentSearchFilter
(
    long? CommentableEntityId,
    long? OwnerId,
    string? Text,
    DateTime? CreatedAt,
    DateTime? UpdatedAt
) : CommentSearchFilter(
    CommentableEntityId,
    OwnerId,
    Text,
    CreatedAt,
    UpdatedAt);
