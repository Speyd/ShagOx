using Microsoft.AspNetCore.Http;

namespace ShagOxServer.Application.DTOs.Advertisements.Core.Update.Images;
public sealed record ImageAdvertUpdateRequest(
    int? Id,
    IFormFile? File,
    int Order,
    bool IsDeleted
);