using ShagOxServer.Application.DTOs.Advertisements.Favorites.Create;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Create;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Create;
public class FavoriteCreateService : IFavoriteCreateService
{
    private readonly IFavoriteRepository _repository;
    private readonly IUserExistsRepository _userRepository;
    private readonly IAdvertisementExistsRepository _advertRepository;

    public FavoriteCreateService(
        IFavoriteRepository favoriteRepository,
        IUserExistsRepository userRepository,
        IAdvertisementExistsRepository advertRepository)
    {
        _repository = favoriteRepository;
        _userRepository = userRepository;
        _advertRepository = advertRepository;
    }

    public async Task<Result<FavoriteCreateResponse>> CreateFavoriteAsync(
        FavoriteCreateRequest request)
    {
        var validationUser = await _userRepository.ExistsAsync(request.UserId);
        if (!validationUser)
            return Result<FavoriteCreateResponse>.NotFound("User");

        var validationAdvert = await _advertRepository.ExistsById(request.AdvertisementId);
        if (!validationAdvert)
            return Result<FavoriteCreateResponse>.NotFound("Advertisement");


        var favorite = CreateFavorite(request);

        await _repository.AddAsync(favorite);

        var response = new FavoriteCreateResponse(
            favorite.Id,
            DateTime.UtcNow
        );

        return Result<FavoriteCreateResponse>.Success(response);
    }

    private Favorite CreateFavorite(
        FavoriteCreateRequest request)
    {
        return new Favorite
        {
            UserId = request.UserId,
            AdvertisementId = request.AdvertisementId,
        };
    }
}
