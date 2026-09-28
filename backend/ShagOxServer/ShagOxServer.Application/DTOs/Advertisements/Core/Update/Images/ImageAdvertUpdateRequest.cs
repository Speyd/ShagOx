using Microsoft.AspNetCore.Http;

namespace ShagOxServer.Application.DTOs.Advertisements.Core.Update.Images;
public sealed record ImageAdvertUpdateRequest(
    long? Id,
    IFormFile? File,
    int Order,
    bool IsDeleted
);