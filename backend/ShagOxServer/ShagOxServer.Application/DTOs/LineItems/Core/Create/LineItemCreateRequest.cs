namespace ShagOxServer.Application.DTOs.LineItems.Core.Create;
public sealed record LineItemCreateRequest
(
    long ParentId,
    long AdvertisementId,
    long? AdvertisementVariantId,
    int Quantity
);
