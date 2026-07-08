using ShagOxServer.Application.DTOs.Advertisements.Favorites.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Advertisements.Favorites.Delete;
public interface IFavoriteDeleteService
{
    Task<Result<FavoriteDeleteResponse>> DeleteFavoriteAsync(
       int id);
}