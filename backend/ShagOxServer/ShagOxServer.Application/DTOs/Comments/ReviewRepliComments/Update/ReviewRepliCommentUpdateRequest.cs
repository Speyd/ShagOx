using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Request;

namespace ShagOxServer.Application.DTOs.Comments.ReviewRepliComments.Update;
public sealed record ReviewRepliCommentUpdateRequest
(
    string? Text,
    List<OwnerPictureOrderUpdateRequest>? Pictures
);
