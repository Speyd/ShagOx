using ShagOxServer.Application.DTOs.Advertisements.Favorites.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Update;
using ShagOxServer.Application.Services.Advertisements.Favorites.Update.Validator;
using ShagOxServer.Application.Services.Advertisements.Favorites.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Update;
public class FavoriteUpdateService : IFavoriteUpdateService
{
    private readonly IFavoriteRepository _repository;
    private readonly FavoriteValidator _favoriteValidator;

    private readonly FavoriteUpdateValidator _favoriteUpdateValidator;

    private readonly IUnitOfWork _unitOfWork;


    public FavoriteUpdateService(
        IFavoriteRepository favoriteRepository,
        FavoriteValidator favoriteValidator,
        FavoriteUpdateValidator favoriteUpdateValidator,
        IUnitOfWork unitOfWork)
    {
        _repository = favoriteRepository;
        _favoriteValidator = favoriteValidator;
        _favoriteUpdateValidator = favoriteUpdateValidator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<FavoriteUpdateResponse>> UpdateFavoriteAsync(
        int favoriteId,
        FavoriteUpdateRequest request)
    {
        var favorite = await _favoriteValidator.GetByIdAsync(favoriteId);
        if (!favorite.IsSuccess)
            return Result<FavoriteUpdateResponse>.Fail(favorite.Error ?? "");

        var validator = await _favoriteUpdateValidator.ValidateAsync(request);
        if(!validator.IsSuccess)
            return Result<FavoriteUpdateResponse>.Fail(validator.Error ?? "");


        var updatedCount = FavoriteUpdater.ApplyUpdates(favorite.Value!, request);
        var result = new FavoriteUpdateResponse(
                DateTime.UtcNow,
                updatedCount
            );

        if (updatedCount == 0)
            return Result<FavoriteUpdateResponse>.Success(result);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _repository.Update(favorite.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<FavoriteUpdateResponse>.Success(result);
    }
}