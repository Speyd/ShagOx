namespace ShagOxServer.Application.DTOs.LineItems.Create;
public sealed record LineItemCreateRequest
(
    long BasketId,
    long AdvertisementId,
    long? AdvertisementVariantId,
    int Quantity
);
