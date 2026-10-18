using Microsoft.AspNetCore.Http;

namespace ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Request;
public record PictureUpdateRequest
(
    long? OwnerId,
    IFormFile? File
);
