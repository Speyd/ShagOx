using ShagOxServer.Application.DTOs.LineItems.LineItemAttributes.Create;
using ShagOxServer.Domain.Entities.LineItems;

namespace ShagOxServer.Application.Services.LineItems.LineItemAttributes.Create;
public static class LineItemAttributeCreater
{
    public static LineItemAttribute Create(
       LineItemAttributeCreateRequest request)
    {
        return new LineItemAttribute
        {
            AttributeDefinitionId = request.AttributeDefinitionId,
            Order = request.Order,
        };
    }
}
