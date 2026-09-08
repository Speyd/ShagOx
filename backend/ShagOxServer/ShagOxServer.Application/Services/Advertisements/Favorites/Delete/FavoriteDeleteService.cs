using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Delete;
using ShagOxServer.Application.Services.Advertisements.Favorites.Validator;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Delete;
public class FavoriteDeleteService 
    : IFavoriteDeleteService
{
    private readonly IRepository<Favorite> _favoriteRepository;
    private readonly FavoriteValidator _favoriteValidator;

    private readonly IUnitOfWork _unitOfWork;


    public FavoriteDeleteService(
        IRepository<Favorite> favoriteRepository,
        FavoriteValidator favoriteValidator,
        IUnitOfWork unitOfWork)
    {
        _favoriteRepository = favoriteRepository;
        _favoriteValidator = favoriteValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        int id, int userId)
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
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               favorite.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}