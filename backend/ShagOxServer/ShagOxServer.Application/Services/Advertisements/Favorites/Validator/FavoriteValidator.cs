using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Validator;
public class FavoriteValidator
{
    private readonly IFavoriteRepository _favoriteRepository;


    public FavoriteValidator(
        IFavoriteRepository favoriteRepository)
    {
        _favoriteRepository = favoriteRepository;
    }

    public async Task<Result<Favorite>> GetFavoriteValidator(
        int favoriteId)
    {
        var favorite = await _favoriteRepository.GetByIdAsync(favoriteId);
        if (favorite is null)
            return Result<Favorite>.NotFound("Favorite");

        return Result<Favorite>.Success(favorite);
    }
}