namespace ShagOxServer.Application.DTOs.LineItems.LineItemAttributes.Update;
public sealed record LineItemAttributeUpdateRequest
(
    long? AttributeDefinitionId,
    int? Order
);
