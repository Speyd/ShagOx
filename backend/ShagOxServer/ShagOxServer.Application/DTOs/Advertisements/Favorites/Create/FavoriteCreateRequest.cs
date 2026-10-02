namespace ShagOxServer.Application.DTOs.Advertisements.Favorites.Create;
public sealed record FavoriteCreateRequest
(
    long UserId,
    long AdvertisementId
);