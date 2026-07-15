using ShagOxServer.Application.DTOs.Advertisements.Favorites.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Delete;
public interface IFavoriteDeleteService
{
    Task<Result<FavoriteDeleteResponse>> DeleteAsync(
       int id, int userId);
}