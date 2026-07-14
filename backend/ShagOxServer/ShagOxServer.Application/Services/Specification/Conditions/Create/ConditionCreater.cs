using ShagOxServer.Application.DTOs.Specification.Conditions.Create;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Services.Specification.Conditions.Create;
public static class ConditionCreater
{
    public static Condition CreateCondition(
       ConditionCreateRequest request)
    {
        return new Condition
        {
            Name = request.Name,
        };
    }
}