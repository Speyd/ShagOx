using ShagOxServer.Application.DTOs.Advertisements.Favorites.Delete;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Delete;
public class FavoriteDeleteService : IFavoriteDeleteService
{
    private readonly IFavoriteRepository _repository;

    private readonly IUnitOfWork _unitOfWork;


    public FavoriteDeleteService(
        IFavoriteRepository favoriteRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = favoriteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<FavoriteDeleteResponse>> DeleteFavoriteAsync(
        int id, int userId)
    {
        var favorite = await _repository.GetByIdAsync(id);
        if (favorite is null)
            return Result<FavoriteDeleteResponse>.NotFound("Favorite");

        if (userId != favorite.UserId)
            return Result<FavoriteDeleteResponse>.Forbidden();

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _repository.Delete(favorite);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<FavoriteDeleteResponse>.Success(
           new FavoriteDeleteResponse(
               favorite.Id,
               DateTime.UtcNow
           )
       );
    }
}