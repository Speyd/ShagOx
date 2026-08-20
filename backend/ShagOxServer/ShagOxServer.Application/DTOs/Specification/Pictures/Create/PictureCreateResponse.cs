using ShagOxServer.Application.DTOs.Common.Responses;

namespace ShagOxServer.Application.DTOs.Specification.Pictures.Create;
public sealed record PictureCreateResponse
(
     int Id,
     string PublicId,
     DateTime CreatedAt
) : CreateResponse(Id, CreatedAt);