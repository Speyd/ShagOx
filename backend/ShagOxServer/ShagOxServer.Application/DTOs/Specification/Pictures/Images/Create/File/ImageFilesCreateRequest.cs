using Microsoft.AspNetCore.Http;

namespace ShagOxServer.Application.DTOs.Specification.Pictures.Images.Create.File;
public sealed record ImageFilesCreateRequest
(
    long AdvertisementId,
    List<IFormFile> Files
);