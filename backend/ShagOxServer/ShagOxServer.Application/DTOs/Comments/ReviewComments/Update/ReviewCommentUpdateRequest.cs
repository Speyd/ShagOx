namespace ShagOxServer.Application.DTOs.Comments.ReviewComments.Update;
public sealed record ReviewCommentUpdateRequest
(
    string? Text,
    int? Grade
);
