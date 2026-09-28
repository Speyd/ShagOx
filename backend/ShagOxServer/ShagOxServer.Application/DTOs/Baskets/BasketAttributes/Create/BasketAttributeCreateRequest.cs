namespace ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Create;
public sealed record BasketAttributeCreateRequest
(
    long AttributeDefinitionId,
    int Order
);