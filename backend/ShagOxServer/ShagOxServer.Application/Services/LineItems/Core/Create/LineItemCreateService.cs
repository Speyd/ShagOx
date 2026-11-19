using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Systems;
using ShagOxServer.Application.DTOs.LineItems.Core.Create;
using ShagOxServer.Application.DTOs.LineItems.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Resources.Order;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Validator;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.LineItems.Core.Validator;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.LineItems.Core;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.LineItems.Core.Create;
public abstract class LineItemCreateService<TEntity>
    where TEntity : LineItem
{
    private readonly LineItemValidator<TEntity> _itemValidator;
    private readonly IUserContext _userContext;
    private readonly AdvertisementVariantValidator _advertVariantValidator;
    private readonly AdvertisementValidator _advertValidator;
    private readonly EntityStatusesOptions _entityOption;


    protected LineItemCreateService(
        LineItemValidator<TEntity> itemValidator,
        IUserContext userContext,
        AdvertisementVariantValidator advertVariantValidator,
        AdvertisementValidator advertValidator,
        IOptions<EntityStatusesOptions> entityOption)
    {
        _itemValidator = itemValidator;
        _userContext = userContext;
        _advertVariantValidator = advertVariantValidator;
        _advertValidator = advertValidator;
        _entityOption = entityOption.Value;
    }


    protected async Task<Result<LineItemDetails>> ValidateAndGetDetailsAsync(
        LineItemCreateRequest request,
        CancellationToken cancellationToken = default)
    {
        var userResult = ValidateUser();

        if (!userResult.IsSuccess)
            return Result<LineItemDetails>.Fail(userResult.Error);

        var advertResult = await GetAdvertisementAsync(
            request,
            cancellationToken);

        if (!advertResult.IsSuccess)
            return Result<LineItemDetails>.Fail(advertResult.Error);

        var (advert, variant) = advertResult.Value!;

        var ownershipResult = ValidateOwnership(advert);

        if (!ownershipResult.IsSuccess)
            return Result<LineItemDetails>.Fail(ownershipResult.Error);

        var availabilityResult = await ValidateProductAvailabilityAsync(
            advert,
            variant,
            cancellationToken);

        if (!availabilityResult.IsSuccess)
            return Result<LineItemDetails>.Fail(availabilityResult.Error);

        var itemResult = await ValidateStatusAsync(
            request,
            cancellationToken);

        if (!itemResult.IsSuccess)
            return Result<LineItemDetails>.Fail(itemResult.Error);

        return ValidateStockAndPrice(advert, variant, request);
    }

    private Result<bool> ValidateUser()
    {
        if (_userContext.UserId is null)
        {
            return Result<bool>.NotFound(EntityNamesResources.User);
        }

        return Result<bool>.Success(true);
    }

    private async Task<Result<(Advertisement? Advert, AdvertisementVariant? Variant)>>
        GetAdvertisementAsync(
            LineItemCreateRequest request,
            CancellationToken cancellationToken)
    {
        if (request.AdvertisementVariantId.HasValue)
        {
            return await GetAdvertisementVariantAsync(
                request,
                cancellationToken);
        }

        var advertResult = await _advertValidator.GetByIdShortIncludeAsync(
            request.AdvertisementId,
            cancellationToken);

        if (!advertResult.IsSuccess || advertResult.Value is null)
        {
            return Result<(Advertisement?, AdvertisementVariant?)>
                .Fail(advertResult.Error);
        }

        return Result<(Advertisement?, AdvertisementVariant?)>.Success(
            (advertResult.Value, null));
    }

    private async Task<Result<(Advertisement? Advert, AdvertisementVariant? Variant)>>
        GetAdvertisementVariantAsync(
            LineItemCreateRequest request,
            CancellationToken cancellationToken)
    {
        var variantResult = await _advertVariantValidator.GetByIdIncludeAsync(
            request.AdvertisementVariantId!.Value,
            cancellationToken);

        if (!variantResult.IsSuccess || variantResult.Value is null)
        {
            return Result<(Advertisement?, AdvertisementVariant?)>
                .Fail(variantResult.Error);
        }

        var variant = variantResult.Value;

        if (request.AdvertisementId != variant.AdvertisementId)
        {
            return Result<(Advertisement?, AdvertisementVariant?)>
                .NotFound(EntityNamesResources.Advertisement);
        }

        return Result<(Advertisement?, AdvertisementVariant?)>.Success(
            (variant.Advertisement, variant));
    }

    private Result<bool> ValidateOwnership(Advertisement? advert)
    {
        if (advert is null)
        {
            return Result<bool>.NotFound(EntityNamesResources.Advertisement);
        }

        if (_userContext.UserId == advert.OwnerId)
        {
            return Result<bool>.Fail(
                EntityErrorResources.CannotAddOwnAdvertisement);
        }

        return Result<bool>.Success(true);
    }

    private async Task<Result<bool>> ValidateProductAvailabilityAsync(
        Advertisement? advert,
        AdvertisementVariant? variant,
        CancellationToken cancellationToken)
    {
        if (advert is not null)
        {
            if (advert.EntityStatus == EntityStatus.Deleted ||
                advert.EntityStatus == EntityStatus.Hidden ||
                advert.Status.Code != _entityOption.ActiveCode)
            {
                return Result<bool>.Fail(string.Format(
                    EntityErrorResources.ProoductNotAvailable,
                    advert.Title));
            }
        }
        else if (variant is not null)
        {
            if (variant.EntityStatus == EntityStatus.Deleted ||
                variant.EntityStatus == EntityStatus.Hidden ||
                variant.Advertisement.Status.Code != _entityOption.ActiveCode)
            {
                return Result<bool>.Fail(string.Format(
                    EntityErrorResources.ProoductNotAvailable,
                    variant.Advertisement.Title));
            }
        }

        return Result<bool>.Success(true);
    }

    private async Task<Result<bool>> ValidateStatusAsync(
        LineItemCreateRequest request,
        CancellationToken cancellationToken)
    {
        return await _itemValidator.NotExistsAsync(
            request.AdvertisementId,
            request.AdvertisementVariantId,
            request.ParentId,
            cancellationToken);
    }

    private Result<LineItemDetails> ValidateStockAndPrice(
        Advertisement? advert,
        AdvertisementVariant? variant,
        LineItemCreateRequest request)
    {
        var stock = variant?.Stock ?? advert?.Stock;
        var price = variant?.Price ?? advert?.Price;
        var title = variant?.Advertisement?.Title ?? advert?.Title;

        if (price is null)
        {
            return Result<LineItemDetails>.Fail(
                string.Format(OrderResources.InvalidPrice, title));
        }

        if (!stock.HasValue || request.Quantity > stock.Value)
        {
            return Result<LineItemDetails>.Fail(
                string.Format(OrderResources.InsufficientStock, title));
        }

        return Result<LineItemDetails>.Success(
            new LineItemDetails(stock.Value, price, advert, variant));
    }
}
