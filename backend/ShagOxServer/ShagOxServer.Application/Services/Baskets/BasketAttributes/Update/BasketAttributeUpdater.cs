using ShagOxServer.Application.DTOs.Baskets.Update;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Baskets.BasketAttributes.Update;
public static class BasketAttributeUpdater
{
    public static int ApplyUpdates(
        BasketAttribute attribute,
        BasketAttributeUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.AttributeDefinitionId.HasValue)
        {
            attribute.AttributeDefinitionId 
                = request.AttributeDefinitionId.Value;

            countUpdated++;
        }

        if (request.Order.HasValue)
        {
            attribute.Order = request.Order.Value;
            countUpdated++;
        }

        return countUpdated;
    }
}