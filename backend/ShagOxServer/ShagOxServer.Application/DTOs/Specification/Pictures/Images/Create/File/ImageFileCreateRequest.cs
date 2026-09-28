using Microsoft.AspNetCore.Http;

namespace ShagOxServer.Application.DTOs.Specification.Pictures.Images.Create.File;
public sealed record ImageFileCreateRequest
(
    IFormFile File,
    long AdvertisementId,
    int Order
);