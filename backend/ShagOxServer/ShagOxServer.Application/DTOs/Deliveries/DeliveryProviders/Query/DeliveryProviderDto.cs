using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Query;
public sealed record DeliveryProviderDto
(
    long Id,
    EntityStatus EntityStatus,
    string Code,
    string? Label
) : BaseDto(Id, EntityStatus);
