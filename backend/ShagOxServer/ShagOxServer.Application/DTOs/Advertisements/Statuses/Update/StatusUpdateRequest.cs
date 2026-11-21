using ShagOxServer.Domain.Entities.Advertisements.Enum;

namespace ShagOxServer.Application.DTOs.Advertisements.Statuses.Update;
public sealed record StatusUpdateRequest
(
    AdvertisementStatus? Code
);
