using ShagOxServer.Application.DTOs.Specification.Conditions;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Services.Specification.Conditions.Mapping;
public static class ConditionMapper
{
    public static ConditionDto ToDto(
        Condition condition)
    {
        return new ConditionDto(
            condition.Id,
            condition.Name
        );
    }
}