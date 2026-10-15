using Microsoft.AspNetCore.Http;

namespace ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update;
public sealed record PictureOrderUpdateRequest
(
    long? OwnerId,
    IFormFile? File,
    int? Order
) : PictureUpdateRequest(OwnerId, File);
