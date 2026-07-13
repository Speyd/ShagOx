using ShagOxServer.Application.DTOs.Advertisements.Favorites.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Create;
using ShagOxServer.Application.Services.Advertisements.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Create;
public class FavoriteCreateService : IFavoriteCreateService
{
    private readonly IFavoriteRepository _repository;

    private readonly IUserExistsRepository _userRepository;

    private readonly AdvertisementValidator _advertValidator;

    private readonly IUnitOfWork _unitOfWork;


    public FavoriteCreateService(
        IFavoriteRepository favoriteRepository,
        IUserExistsRepository userRepository,
        AdvertisementValidator advertValidator,
        IUnitOfWork unitOfWork)
    {
        _repository = favoriteRepository;
        _userRepository = userRepository;
        _advertValidator = advertValidator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<FavoriteCreateResponse>> CreateFavoriteAsync(
        FavoriteCreateRequest request)
    {
        var validationUser = await _userRepository.ExistsAsync(request.UserId);
        if (!validationUser)
            return Result<FavoriteCreateResponse>.NotFound("User");

        var validationAdvert = await _advertValidator.ExistsAdvertisementValidator(
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