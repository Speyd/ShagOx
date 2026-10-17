namespace ShagOxServer.Application.DTOs.Comments.ReviewComments.Create;
public sealed record ReviewCommentCreateRequest
(
    long CommentableEntityId,
    string Text,
    int Grade
);
