namespace ShagOxServer.Application.DTOs.LineItems.Create;
public sealed record LineItemCreateRequest
(
    long ParentId,
    long AdvertisementId,
    long? AdvertisementVariantId,
    int Quantity
);
