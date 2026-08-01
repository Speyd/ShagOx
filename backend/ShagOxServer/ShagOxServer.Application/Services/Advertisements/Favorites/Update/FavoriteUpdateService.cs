using ShagOxServer.Application.DTOs.Advertisements.Favorites.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Update;
using ShagOxServer.Application.Services.Advertisements.Favorites.Update.Validator;
using ShagOxServer.Application.Services.Advertisements.Favorites.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.Application.DTOs.Common.Responses;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Update;
public class FavoriteUpdateService : IFavoriteUpdateService
{
    private readonly IFavoriteRepository _favoriteRepository;
    private readonly FavoriteValidator _favoriteValidator;
    private readonly FavoriteUpdateValidator _favoriteUpdateValidator;

    private readonly IUnitOfWork _unitOfWork;


    public FavoriteUpdateService(
        IFavoriteRepository favoriteRepository,
        FavoriteValidator favoriteValidator,
        FavoriteUpdateValidator favoriteUpdateValidator,
        IUnitOfWork unitOfWork)
    {
        _favoriteRepository = favoriteRepository;
        _favoriteValidator = favoriteValidator;
        _favoriteUpdateValidator = favoriteUpdateValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int favoriteId,
        FavoriteUpdateRequest request)
    {
        var favorite = await _favoriteValidator.GetByIdAsync(favoriteId);
        if (!favorite.IsSuccess)
            return Result<UpdateResponse>.Fail(favorite.Error);

        var validator = await _favoriteUpdateValidator.ValidateAsync(request);
        if(!validator.IsSuccess)
            return Result<UpdateResponse>.Fail(validator.Error);


        var updatedCount = FavoriteUpdater
            .ApplyUpdates(favorite.Value!, request);

        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(result);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _favoriteRepository.Update(favorite.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<UpdateResponse>.Success(result);
    }
}