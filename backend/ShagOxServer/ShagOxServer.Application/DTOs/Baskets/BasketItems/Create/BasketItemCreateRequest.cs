namespace ShagOxServer.Application.DTOs.Baskets.BasketItems.Create;
public sealed record BasketItemCreateRequest
(
    long BasketId,
    long AdvertisementVariantId,
    int Quantity
);