using Microsoft.AspNetCore.Http;

namespace ShagOxServer.Application.DTOs.Specification.Images.Create.File;
public sealed record ImageFilesCreateRequest
(
    int AdvertisementId,
    List<IFormFile> Files
);