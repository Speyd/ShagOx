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


    public Task<Result<bool>> ValidateAsync(
        FavoriteUpdateRequest request,
        CancellationToken cancellationToken = default)
    {
        return ValidateFavoriteAsync(request.AdvertisementId);
    }

    private async Task<Result<bool>> ValidateFavoriteAsync(
        long? advertisementId,
        CancellationToken cancellationToken = default)
    {
        if (advertisementId is null)
            return Result<bool>.Success(true);

        var advert = await _advertisementValidator
            .GetByIdAsync(advertisementId.Value, cancellationToken);

        if (!advert.IsSuccess)
            return Result<bool>.NotFound(
                EntityNamesResources.Advertisement);

        return Result<bool>.Success(true);
    }
}
