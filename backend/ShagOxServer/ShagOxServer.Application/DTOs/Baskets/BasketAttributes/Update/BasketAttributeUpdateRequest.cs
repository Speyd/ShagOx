namespace ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Update;
public sealed record BasketAttributeUpdateRequest
(
    int? AttributeDefinitionId,
    int? Order
);