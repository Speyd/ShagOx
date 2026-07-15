using Microsoft.AspNetCore.Http;

namespace ShagOxServer.Application.DTOs.Specification.Images.Create.File;
public sealed record ImageFileCreateRequest
(
    IFormFile File,
    int AdvertisementId,
    int Order
);