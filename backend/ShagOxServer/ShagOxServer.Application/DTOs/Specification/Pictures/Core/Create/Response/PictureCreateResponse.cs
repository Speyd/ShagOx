using ShagOxServer.Application.DTOs.Base.Responses;

namespace ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Response;
public sealed record PictureCreateResponse
(
     long Id,
     string PublicId,
     DateTime CreatedAt
) : CreateResponse(Id, CreatedAt);
