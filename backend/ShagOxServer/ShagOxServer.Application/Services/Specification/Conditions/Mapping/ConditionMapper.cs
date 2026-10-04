using ShagOxServer.Application.DTOs.Specification.Conditions.Query;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Services.Specification.Conditions.Mapping;
public static class ConditionMapper
{
    public static ConditionDto ToDto(
        Condition condition,
        string? lable)
    {
        return new ConditionDto(
            condition.Id,
            condition.Code,
            lable
        );
    }
}