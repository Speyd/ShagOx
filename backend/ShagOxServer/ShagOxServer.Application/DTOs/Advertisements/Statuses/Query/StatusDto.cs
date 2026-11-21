using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements.Enum;

namespace ShagOxServer.Application.DTOs.Advertisements.Statuses.Query;
public sealed record StatusDto
(
    long Id,
    EntityStatus EntityStatus,
    AdvertisementStatus Code,
    string? Label
) : BaseDto(Id, EntityStatus);
