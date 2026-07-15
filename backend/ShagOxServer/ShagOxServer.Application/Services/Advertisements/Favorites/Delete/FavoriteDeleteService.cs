using ShagOxServer.Application.DTOs.Advertisements.Favorites.Delete;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Delete;
using ShagOxServer.Application.Services.Advertisements.Favorites.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Delete;
public class FavoriteDeleteService : IFavoriteDeleteService
{
    private readonly IFavoriteRepository _favoriteRepository;
    private readonly FavoriteValidator _favoriteValidator;

    private readonly IUnitOfWork _unitOfWork;


    public FavoriteDeleteService(
        IFavoriteRepository favoriteRepository,
        FavoriteValidator favoriteValidator,
        IUnitOfWork unitOfWork)
    {
        _favoriteRepository = favoriteRepository;
        _favoriteValidator = favoriteValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<FavoriteDeleteResponse>> DeleteAsync(
        int id, int userId)
    {
        var favorite = await _favoriteValidator.GetByIdAsync(id);
        if (!favorite.IsSuccess)
            return Result<FavoriteDeleteResponse>.Fail(favorite.Error ?? "");

        if (userId != favorite.Value!.UserId)
            return Result<FavoriteDeleteResponse>.Forbidden();

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _favoriteRepository.Delete(favorite.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<FavoriteDeleteResponse>.Success(
           new FavoriteDeleteResponse(
               favorite.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}