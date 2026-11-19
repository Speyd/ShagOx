using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Specification.Pictures.CategoryImages.Query;
public sealed record CategoryImageDto
(
    long Id,
    EntityStatus EntityStatus,
    string Url,
    string PublicId,
    long CategoryId
) : BaseImageDto(Id, EntityStatus, Url, PublicId);
