using ShagOxServer.Application.DTOs.Advertisements.Favorites.Update;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Update;
public interface IFavoriteUpdateService
{
    Task<Result<FavoriteUpdateResponse>> UpdateFavoriteAsync(
        int favoriteId,
        FavoriteUpdateRequest request);
}