using ShagOxServer.Application.DTOs.Base.Responses;

namespace ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Response;
public sealed record PictureUpdateResponse
(
     int CountUpdated,
     string? OldPublicId,
     string? OldUrl,
     string? NewPublicId,
     string? NewUrl,
     DateTime UpdateAt
) : UpdateResponse(CountUpdated, UpdateAt);
