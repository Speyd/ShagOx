using ShagOxServer.Domain.Entities.Advertisements.Enum;

namespace ShagOxServer.Application.DTOs.Advertisements.Statuses.Create;
public sealed record StatusCreateRequest
(
    AdvertisementStatus Code
);
