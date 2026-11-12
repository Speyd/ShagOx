namespace ShagOxServer.Application.DTOs.LineItems.LineItemAttributes.Create;
public sealed record LineItemAttributeCreateRequest
(
    long AttributeDefinitionId,
    int Order
);
