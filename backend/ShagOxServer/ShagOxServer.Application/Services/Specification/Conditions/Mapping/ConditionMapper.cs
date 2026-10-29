using ShagOxServer.Application.DTOs.Specification.Conditions.Query;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Services.Specification.Conditions.Mapping;
public static class ConditionMapper
{
    public static ConditionDto ToDto(
        Condition condition,
        string? label)
    {
        return new ConditionDto(
            condition.Id,
            condition.Code,
            label
        );
    }
}
