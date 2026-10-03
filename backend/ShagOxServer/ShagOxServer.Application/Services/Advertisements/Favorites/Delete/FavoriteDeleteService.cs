using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Delete;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Advertisements.Favorites.Mapping;
using ShagOxServer.Application.Services.Advertisements.Favorites.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
using ShagOxServer.Domain.Caches.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Delete;
public class FavoriteDeleteService 
    : IFavoriteDeleteService
{
    private readonly IRepository<Favorite> _favoriteRepository;
    private readonly FavoriteValidator _favoriteValidator;

    private readonly FavoriteInvalidationService _favoriteInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<FavoriteDeleteService> _logger;


    public FavoriteDeleteService(
        IRepository<Favorite> favoriteRepository,
        FavoriteValidator favoriteValidator,
        FavoriteInvalidationService favoriteInvalid,
        IUnitOfWork unitOfWork,
        ILogger<FavoriteDeleteService> logger)
    {
        _favoriteRepository = favoriteRepository;
        _favoriteValidator = favoriteValidator;
        _favoriteInvalid = favoriteInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id, 
        long userId)
    {
        var favorite = await _favoriteValidator
            .GetByIdAsync(id);

        if (!favorite.IsSuccess)
            return Result<DeleteResponse>.Fail(favorite.Error);

        if (userId != favorite.Value!.UserId)
            return Result<DeleteResponse>.Forbidden();

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _favoriteRepository.Delete(favorite.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to delete favorite. " +
                "UserId: {Id}, AdvertisementId: {AdvertisementId}",
                userId,
                id);

            return Result<DeleteResponse>
                    .Fail(EntityErrorResources.FavoriteDeleteFailed);
        }

        _logger.LogInformation(
            "Favorite delete successfully. Id: {Id}",
            favorite.Value!.Id);

        await _favoriteInvalid.InvalidateDeleteAsync(
            FavoriteCacheMapper.ToList(favorite.Value!));

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               favorite.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}