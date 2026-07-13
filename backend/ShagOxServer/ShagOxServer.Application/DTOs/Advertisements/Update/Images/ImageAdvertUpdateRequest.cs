using Microsoft.AspNetCore.Http;

namespace ShagOxServer.Application.DTOs.Advertisements.Update.Images;
public sealed record ImageAdvertUpdateRequest(
    int? Id,
    IFormFile? File,
    int Order,
    bool IsDeleted
);