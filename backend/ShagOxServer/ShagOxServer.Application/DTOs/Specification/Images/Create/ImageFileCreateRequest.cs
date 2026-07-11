using Microsoft.AspNetCore.Http;

namespace ShagOxServer.Application.DTOs.Specification.Images.Create;
public sealed record ImageFileCreateRequest
(
    IFormFile File,
    int AdvertisementId,
    int Order
);