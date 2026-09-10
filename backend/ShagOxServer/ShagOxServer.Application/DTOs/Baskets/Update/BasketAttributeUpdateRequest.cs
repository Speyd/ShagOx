namespace ShagOxServer.Application.DTOs.Baskets.Update;
public sealed record BasketAttributeUpdateRequest
(
    int? AttributeDefinitionId,
    int? Order
);