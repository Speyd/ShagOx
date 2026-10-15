using Microsoft.AspNetCore.Http;

namespace ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update;
public sealed record OwnerPictureOrderUpdateRequest(
    long? Id,
    IFormFile? File,
    int Order,
    bool IsDeleted
);
