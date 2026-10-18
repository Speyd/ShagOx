using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update;

namespace ShagOxServer.Application.DTOs.Comments.ReviewComments.Update;
public sealed record ReviewCommentUpdateRequest
(
    string? Text,
    int? Grade,
    List<OwnerPictureOrderUpdateRequest>? Pictures
);
