using ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Create;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Baskets.BasketAttributes.Create;
public static class BasketAttributeCreater
{
    public static BasketAttribute Create(
       BasketAttributeCreateRequest request)
    {
        return new BasketAttribute
        {
            AttributeDefinitionId = request.AttributeDefinitionId,
            Order = request.Order,
        };
    }
}