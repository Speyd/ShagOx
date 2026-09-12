namespace ShagOxServer.Application.DTOs.Baskets.BasketItems.Create;
public sealed record BasketItemCreateRequest
(
    int BasketId,
    int AdvertisementId,
    int Quantity
);