using ShagOxServer.Application.DTOs.Advertisements.Favorites.Update;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Update.Validator;
public class FavoriteUpdateValidator
{
    private readonly UserValidator _userValidator;
    private readonly AdvertisementValidator _advertisementValidator;


    public FavoriteUpdateValidator(
        UserValidator userValidator,
        AdvertisementValidator advertisementValidator)
    {
        _userValidator = userValidator;
        _advertisementValidator = advertisementValidator;
    }


    public async Task<Result<bool>> ValidateAsync(
        FavoriteUpdateRequest request)
    {
        if (request.UserId is not null)
        {
            var user = await _userValidator
                .GetByIdAsync(request.UserId.Value);

            if (!user.IsSuccess)
                return Result<bool>.NotFound(
                    EntityNamesResources.User);
        }


        if (request.AdvertisementId is not null)
        {
            var advert = await _advertisementValidator
                .GetByIdAsync(request.AdvertisementId.Value);

            if (!advert.IsSuccess)
                return Result<bool>.NotFound(
                    EntityNamesResources.Advertisement);
        }

        return Result<bool>.Success(true);
    }
}