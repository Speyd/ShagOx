using ShagOxServer.Application.DTOs.Specification.Conditions.Update;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Services.Specification.Conditions.Update;
public static class ConditionUpdater
{
    public static int ApplyUpdates(
        Condition condition,
        ConditionUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.Code is not null)
        {
            condition.Code = request.Code;
            countUpdated++;
        }


        return countUpdated;
    }
}