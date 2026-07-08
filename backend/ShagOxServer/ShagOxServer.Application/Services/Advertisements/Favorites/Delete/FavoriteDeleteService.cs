using ShagOxServer.Application.DTOs.Advertisements.Favorites.Delete;
using ShagOxServer.Application.Interfaces.Advertisements.Favorites.Delete;
using ShagOxServer.Infrastructure.Interfaces.Advertisements.Favorites;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Delete;
public class FavoriteDeleteService : IFavoriteDeleteService
{
    private readonly IFavoriteRepository _repository;


    public FavoriteDeleteService(
        IFavoriteRepository favoriteRepository)
    {
        _repository = favoriteRepository;
    }

    public async Task<Result<FavoriteDeleteResponse>> DeleteFavoriteAsync(
        int id, int userId)
    {
        var favorite = await _repository.GetByIdAsync(id);
        if (favorite is null)
            return Result<FavoriteDeleteResponse>.NotFound("Favorite");

        if (userId != favorite.UserId)
            return Result<FavoriteDeleteResponse>.Forbidden();

        await _repository.DeleteAsync(favorite);
        return Result<FavoriteDeleteResponse>.Success(
           new FavoriteDeleteResponse(
               favorite.Id,
               DateTime.UtcNow
           )
       );
    }
}