using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Advertisements.Favorites.Validator;
using ShagOxServer.Application.Services.Base.Delete;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Delete;
public class FavoriteDeleteService
    : BaseDeleteService<Favorite>
    , IFavoriteDeleteService
{
    private readonly IRepository<Favorite> _favoriteRepository;
    private readonly FavoriteValidator _favoriteValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<FavoriteDeleteService> _logger;


    public FavoriteDeleteService(
        IRepository<Favorite> favoriteRepository,
        ICommandRepository<Favorite> favoriteCommand,
        FavoriteValidator favoriteValidator,
        FavoriteInvalidationService favoriteInvalid,
        IUnitOfWork unitOfWork,
        ILogger<FavoriteDeleteService> logger
    )
        : base(favoriteCommand, favoriteValidator, unitOfWork, logger)
    {
        _favoriteRepository = favoriteRepository;
        _favoriteValidator = favoriteValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        var favorite = await _favoriteValidator
            .GetByIdAsync(id, cancellationToken);

        if (!favorite.IsSuccess)
            return Result<DeleteResponse>.Fail(favorite.Error);

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
                favorite.Value?.OwnerId,
                id);

            return Result<DeleteResponse>
                    .Fail(EntityErrorResources.FavoriteDeleteFailed);
        }

        _logger.LogInformation(
            "Favorite delete successfully. Id: {Id}",
            favorite.Value!.Id);

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               favorite.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}
