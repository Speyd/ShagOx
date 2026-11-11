namespace ShagOxServer.Application.DTOs.Baskets.BasketItems.Create;
public sealed record BasketItemCreateRequest
(
    long BasketId,
    long AdvertisementId,
    long? AdvertisementVariantId,
    int Quantity
);
