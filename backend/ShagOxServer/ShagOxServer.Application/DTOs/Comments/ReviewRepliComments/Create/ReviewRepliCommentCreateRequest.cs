using Microsoft.AspNetCore.Http;

namespace ShagOxServer.Application.DTOs.Comments.ReviewRepliComments.Create;
public sealed record ReviewRepliCommentCreateRequest
(
    long CommentableEntityId,
    long? RepliId,
    string Text,
    List<IFormFile> Pictures
);
