using ShagOxServer.Application.DTOs.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Translations.Query;
public sealed record DeliveryProviderTranslationDto
(
    long Id,
    EntityStatus EntityStatus,
    string Name
) : BaseDto(Id, EntityStatus);
