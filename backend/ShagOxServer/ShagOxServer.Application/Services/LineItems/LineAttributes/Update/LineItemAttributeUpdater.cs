using ShagOxServer.Application.DTOs.LineItems.LineItemAttributes.Update;
using ShagOxServer.Domain.Entities.LineItems;

namespace ShagOxServer.Application.Services.LineItems.LineItemAttributes.Update;
public static class LineItemAttributeUpdater
{
    public static int ApplyUpdates(
        LineItemAttribute attribute,
        LineItemAttributeUpdateRequest request)
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
