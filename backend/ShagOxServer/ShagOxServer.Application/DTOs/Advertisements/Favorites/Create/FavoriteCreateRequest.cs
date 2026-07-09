namespace ShagOxServer.Application.DTOs.Advertisements.Favorites.Create;
public sealed record FavoriteCreateRequest
(   
    int UserId,
    int AdvertisementId
);