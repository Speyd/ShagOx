using Microsoft.AspNetCore.Http;

namespace ShagOxServer.Application.DTOs.Comments.ReviewRepliComments.Create;
public sealed record ReviewRepliCommentCreateRequest
(
    long CommentableEntityId,
    string Text,
    List<IFormFile> Pictures
);
