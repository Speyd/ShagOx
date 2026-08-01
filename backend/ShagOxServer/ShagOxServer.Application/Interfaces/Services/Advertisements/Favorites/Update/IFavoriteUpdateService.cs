using ShagOxServer.Application.DTOs.Advertisements.Favorites.Update;
using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Update;
public interface IFavoriteUpdateService
{
    Task<Result<UpdateResponse>> UpdateAsync(
        int favoriteId,
        FavoriteUpdateRequest request);
}