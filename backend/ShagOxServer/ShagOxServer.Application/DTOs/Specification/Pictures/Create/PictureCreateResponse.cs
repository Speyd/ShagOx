using ShagOxServer.Application.DTOs.Base.Responses;

namespace ShagOxServer.Application.DTOs.Specification.Pictures.Create;
public sealed record PictureCreateResponse
(
     int Id,
     string PublicId,
     DateTime CreatedAt
) : CreateResponse(Id, CreatedAt);