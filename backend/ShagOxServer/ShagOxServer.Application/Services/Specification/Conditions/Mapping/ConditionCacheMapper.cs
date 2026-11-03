using ShagOxServer.Application.DTOs.Specification.Conditions.Cache;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Services.Specification.Conditions.Mapping;
public static class ConditionCacheMapper
{
    public static ConditionCacheInfo ToInfo(
        Condition condition,
        EntityStatus status = EntityStatus.Active)
    {
        return new ConditionCacheInfo(
            condition.Id,
            status
        );
    }
}

