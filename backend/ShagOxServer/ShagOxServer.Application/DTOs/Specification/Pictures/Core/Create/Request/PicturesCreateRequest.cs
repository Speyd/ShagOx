
using Microsoft.AspNetCore.Http;

namespace ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
public sealed record PicturesCreateRequest
(
    long OwnerId,
    List<IFormFile> Files
);
