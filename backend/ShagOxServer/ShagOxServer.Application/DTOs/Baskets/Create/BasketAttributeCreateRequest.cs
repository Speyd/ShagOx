namespace ShagOxServer.Application.DTOs.Baskets.Create;
public sealed record BasketAttributeCreateRequest
(
    int AttributeDefinitionId,
    int Order
);