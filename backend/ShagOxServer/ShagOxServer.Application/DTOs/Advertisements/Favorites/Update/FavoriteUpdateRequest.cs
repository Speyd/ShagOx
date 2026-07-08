namespace ShagOxServer.Application.DTOs.Advertisements.Favorites.Update;
public sealed record FavoriteUpdateRequest
(
    int? UserId,
    int? AdvertisementId
);