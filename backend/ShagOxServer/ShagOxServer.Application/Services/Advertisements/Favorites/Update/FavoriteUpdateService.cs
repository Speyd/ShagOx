using ShagOxServer.Application.DTOs.Advertisements.Favorites.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Update;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Update;
public class FavoriteUpdateService : IFavoriteUpdateService
{
    private readonly IFavoriteRepository _repository;

    private readonly IUserExistsRepository _userRepository;

    private readonly IAdvertisementExistsRepository _advertRepository;

    private readonly IUnitOfWork _unitOfWork;


    public FavoriteUpdateService(
        IFavoriteRepository favoriteRepository,
        IUserExistsRepository userRepository,
        IAdvertisementExistsRepository advertRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = favoriteRepository;
        _userRepository = userRepository;
        _advertRepository = advertRepository;
        _unitOfWork = unitOfWork;
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

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _repository.Update(favorite);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

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