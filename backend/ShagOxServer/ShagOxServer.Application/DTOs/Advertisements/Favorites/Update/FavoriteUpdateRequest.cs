namespace ShagOxServer.Application.DTOs.Advertisements.Favorites.Update;
public sealed record FavoriteUpdateRequest
(
    long? UserId,
    long? AdvertisementId
);