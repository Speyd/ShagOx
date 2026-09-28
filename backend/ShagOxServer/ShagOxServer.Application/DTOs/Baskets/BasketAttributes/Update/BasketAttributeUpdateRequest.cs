namespace ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Update;
public sealed record BasketAttributeUpdateRequest
(
    long? AttributeDefinitionId,
    int? Order
);