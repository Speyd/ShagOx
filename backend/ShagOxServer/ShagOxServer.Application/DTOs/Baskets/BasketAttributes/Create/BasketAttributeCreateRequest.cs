namespace ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Create;
public sealed record BasketAttributeCreateRequest
(
    int AttributeDefinitionId,
    int Order
);