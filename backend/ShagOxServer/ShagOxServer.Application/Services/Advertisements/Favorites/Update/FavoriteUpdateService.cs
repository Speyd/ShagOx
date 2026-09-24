using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Advertisements.Favorites.Update;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Update;
using ShagOxServer.Application.Services.Advertisements.Favorites.Update.Validator;
using ShagOxServer.Application.Services.Advertisements.Favorites.Validator;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Update;
public class FavoriteUpdateService 
    : IFavoriteUpdateService
{
    private readonly IRepository<Favorite> _favoriteRepository;
    private readonly FavoriteValidator _favoriteValidator;
    private readonly FavoriteUpdateValidator _favoriteUpdateValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<FavoriteUpdateService> _logger;


    public FavoriteUpdateService(
        IRepository<Favorite> favoriteRepository,
        FavoriteValidator favoriteValidator,
        FavoriteUpdateValidator favoriteUpdateValidator,
        IUnitOfWork unitOfWork,
        ILogger<FavoriteUpdateService> logger)
    {
        _favoriteRepository = favoriteRepository;
        _favoriteValidator = favoriteValidator;
        _favoriteUpdateValidator = favoriteUpdateValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
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
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to update favorite. " +
                "FavoriteId: {FavoriteIdId}, " +
                "UserId: {UserId}, AdvertisementId: {AdvertisementId}",
                favoriteId,
                request.UserId,
                request.AdvertisementId);

            return Result<UpdateResponse>
                   .Fail("Failed to update favorite.");
        }

        _logger.LogInformation(
            "Favorite update successfully. Id: {Id}",
            favorite.Value!.Id);

        return Result<UpdateResponse>.Success(result);
    }
}