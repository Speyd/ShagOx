using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.DTOs.LineItems.LineItemAttributes.Cache;
public sealed record LineItemAttributeCacheInfo
(
    long Id,
    long CategoryId,
    long AttributeDefinitionId,
    EntityStatus Status = EntityStatus.Active
);
