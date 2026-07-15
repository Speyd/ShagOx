using ShagOxServer.Application.DTOs.Advertisements.Favorites.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Create;
using ShagOxServer.Application.Services.Advertisements.Validator;
using ShagOxServer.Application.Services.Users.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Create;
public class FavoriteCreateService : IFavoriteCreateService
{
    private readonly IFavoriteRepository _repository;

    private readonly UserValidator _userValidator;

    private readonly AdvertisementValidator _advertValidator;

    private readonly IUnitOfWork _unitOfWork;


    public FavoriteCreateService(
        IFavoriteRepository favoriteRepository,
        UserValidator userValidator,
        AdvertisementValidator advertValidator,
        IUnitOfWork unitOfWork)
    {
        _repository = favoriteRepository;
        _userValidator = userValidator;
        _advertValidator = advertValidator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<FavoriteCreateResponse>> CreateFavoriteAsync(
        FavoriteCreateRequest request)
    {
        var validationUser = await _userValidator.ExistsByIdAsync(request.UserId);
        if (!validationUser.IsSuccess)
            return Result<FavoriteCreateResponse>.Fail(validationUser.Error ?? "");

        var validationAdvert = await _advertValidator.ExistsByIdAsync(
            request.AdvertisementId);

        if (!validationAdvert.IsSuccess)
            return Result<FavoriteCreateResponse>.Fail(validationAdvert.Error ?? "");

        var favorite = FavoriteCreater.CreateFavorite(request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _repository.Add(favorite);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<FavoriteCreateResponse>.Success(
            new FavoriteCreateResponse(
            favorite.Id,
            DateTime.UtcNow
        ));
    }
}