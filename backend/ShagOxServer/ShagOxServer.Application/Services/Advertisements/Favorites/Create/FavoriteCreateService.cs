using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Advertisements.Favorites.Create;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Create;
using ShagOxServer.Application.Resources.EntityErrorResourcess;
using ShagOxServer.Application.Services.Advertisements.Core.Delete;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
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
    private readonly ILogger<FavoriteCreateService> _logger;


    public FavoriteCreateService(
        IRepository<Favorite> favoriteRepository,
        UserValidator userValidator,
        AdvertisementValidator advertValidator,
        IUnitOfWork unitOfWork,
        ILogger<FavoriteCreateService> logger)
    {
        _favoriteRepository = favoriteRepository;
        _userValidator = userValidator;
        _advertValidator = advertValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
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
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create favorite. " +
                "UserId: {Id}, AdvertisementId: {AdvertisementId}",
                request.UserId,
                request.AdvertisementId);

            return Result<CreateResponse>
                    .Fail(EntityErrorResources.FavoriteCreateFailed);
        }

        _logger.LogInformation(
            "Favorite create successfully. Id: {Id}",
            favorite.Id);

        return Result<CreateResponse>.Success(
            new CreateResponse(
                favorite.Id,
                DateTime.UtcNow
        ));
    }
}