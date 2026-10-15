using Microsoft.AspNetCore.Http;

namespace ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
public sealed record PictureOrderCreateRequest
(
    IFormFile File,
    long OwnerId,
    int Order
) : PictureCreateRequest(OwnerId, File);
