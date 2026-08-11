using ShagOxServer.Application.DTOs.Advertisements.Favorites.Create;
using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Create;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.Auth.Users.Validator;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Create;
public class FavoriteCreateService 
    : IFavoriteCreateService
{
    private readonly IRepository<Favorite> _favoriteRepository;

    private readonly UserValidator _userValidator;

    private readonly AdvertisementValidator _advertValidator;

    private readonly IUnitOfWork _unitOfWork;


    public FavoriteCreateService(
        IRepository<Favorite> favoriteRepository,
        UserValidator userValidator,
        AdvertisementValidator advertValidator,
        IUnitOfWork unitOfWork)
    {
        _favoriteRepository = favoriteRepository;
        _userValidator = userValidator;
        _advertValidator = advertValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        FavoriteCreateRequest request)
    {
        var validationUser = await _userValidator
            .ExistsByIdAsync(request.UserId);

        if (!validationUser.IsSuccess)
            return Result<CreateResponse>.Fail(validationUser.Error);


        var validationAdvert = await _advertValidator.ExistsByIdAsync(
            request.AdvertisementId);

        if (!validationAdvert.IsSuccess)
            return Result<CreateResponse>.Fail(validationAdvert.Error);

        var favorite = FavoriteCreater.Create(request);
        

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _favoriteRepository.Add(favorite);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<CreateResponse>.Success(
            new CreateResponse(
                favorite.Id,
                DateTime.UtcNow
        ));
    }
}