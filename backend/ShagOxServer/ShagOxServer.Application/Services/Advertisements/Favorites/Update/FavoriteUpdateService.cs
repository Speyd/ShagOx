using ShagOxServer.Application.DTOs.Advertisements.Favorites.Update;
using ShagOxServer.Application.Interfaces.Advertisements.Favorites.Update;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Infrastructure.Interfaces.Advertisements;
using ShagOxServer.Infrastructure.Interfaces.Advertisements.Favorites;
using ShagOxServer.Infrastructure.Interfaces.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Update;
public class FavoriteUpdateService : IFavoriteUpdateService
{
    private readonly IFavoriteRepository _repository;
    private readonly IUserExistsRepository _userRepository;
    private readonly IAdvertisementExistsRepository _advertRepository;

    public FavoriteUpdateService(
        IFavoriteRepository favoriteRepository,
        IUserExistsRepository userRepository,
        IAdvertisementExistsRepository advertRepository)
    {
        _repository = favoriteRepository;
        _userRepository = userRepository;
        _advertRepository = advertRepository;
    }

    public async Task<Result<FavoriteUpdateResponse>> UpdateFavoriteAsync(
        int favoriteId,
        FavoriteUpdateRequest request)
    {
        var favorite = await _repository.GetByIdAsync(favoriteId);
        if (favorite is null)
            return Result<FavoriteUpdateResponse>.NotFound("Favorite");


        if (request.UserId is not null &&
            !(await _userRepository.ExistsAsync(request.UserId.Value)))
        {
            return Result<FavoriteUpdateResponse>.AlreadyExists("User");
        }
        else if (request.AdvertisementId is not null &&
            !(await _advertRepository.ExistsById(request.AdvertisementId.Value)))
        {
            return Result<FavoriteUpdateResponse>.AlreadyExists("Advertisement");
        }

        var updatedCount = ApplyUpdates(favorite, request);
        var result = new FavoriteUpdateResponse(
                DateTime.UtcNow,
                updatedCount
            );

        if (updatedCount == 0)
            return Result<FavoriteUpdateResponse>.Success(result);

        await _repository.UpdateAsync(favorite);

        return Result<FavoriteUpdateResponse>.Success(result);
    }

    private static int ApplyUpdates(
        Favorite favorite,
        FavoriteUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.UserId is not null)
        {
            favorite.UserId = request.UserId.Value;
            countUpdated++;
        }

        if (request.AdvertisementId is not null)
        {
            favorite.AdvertisementId = request.AdvertisementId.Value;
            countUpdated++;
        }

        return countUpdated;
    }
}
