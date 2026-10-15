using Microsoft.AspNetCore.Http;

namespace ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update;
public record PictureUpdateRequest
(
    long? OwnerId,
    IFormFile? File
);
