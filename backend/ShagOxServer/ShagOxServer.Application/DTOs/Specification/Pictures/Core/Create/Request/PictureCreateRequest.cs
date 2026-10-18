using Microsoft.AspNetCore.Http;

namespace ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
public record PictureCreateRequest
(
    long OwnerId,
    IFormFile File
);
