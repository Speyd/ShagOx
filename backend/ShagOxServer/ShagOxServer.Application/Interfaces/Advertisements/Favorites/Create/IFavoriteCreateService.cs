using ShagOxServer.Application.DTOs.Advertisements.Favorites.Create;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Advertisements.Favorites.Create;
public interface IFavoriteCreateService
{
    Task<Result<FavoriteCreateResponse>> CreateFavoriteAsync(
       FavoriteCreateRequest request);
}