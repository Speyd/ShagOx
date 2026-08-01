using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Validator;
public class FavoriteValidator
{
    private readonly IFavoriteRepository _favoriteRepository;
    private readonly IFavoriteExistsRepository _favoriteExistsRepository;


    public FavoriteValidator(
        IFavoriteRepository favoriteRepository,
        IFavoriteExistsRepository favoriteExistsRepository)
    {
        _favoriteRepository = favoriteRepository;
        _favoriteExistsRepository = favoriteExistsRepository;
    }

    public async Task<Result<Favorite>> GetByIdAsync(
        int favoriteId)
    {
        var favorite = await _favoriteRepository
            .GetByIdAsync(favoriteId);

        if (favorite is null)
            return Result<Favorite>.NotFound("Favorite");

        return Result<Favorite>.Success(favorite);
    }

    public async Task<Result<bool>> ExistsByIdAsync(
        int favoriteId)
    {
        if (!await _favoriteExistsRepository
            .ExistsByIdAsync(favoriteId))

            return Result<bool>.NotFound("Favorite");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByIdAsync(
        int favoriteId)
    {
        if (await _favoriteExistsRepository
            .ExistsByIdAsync(favoriteId))

            return Result<bool>.AlreadyExists("Favorite");

        return Result<bool>.Success(true);
    }
}