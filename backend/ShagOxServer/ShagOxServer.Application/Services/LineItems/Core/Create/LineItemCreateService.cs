using ShagOxServer.Application.DTOs.LineItems.Core.Create;
using ShagOxServer.Application.DTOs.LineItems.Core.Query;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Resources.Order;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Validator;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.LineItems.Core.Validator;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.LineItems.Core;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.LineItems.Core.Create;
public abstract class LineItemCreateService<TEntity>
    where TEntity : LineItem
{
    private readonly LineItemValidator<TEntity> _itemValidator;

    private readonly AdvertisementVariantValidator _advertVariantValidator;
    private readonly AdvertisementValidator _advertValidator;


    public LineItemCreateService(
        LineItemValidator<TEntity> itemValidator,
        AdvertisementVariantValidator advertVariantValidator,
        AdvertisementValidator advertValidator)
    {
        _itemValidator = itemValidator;
        _advertVariantValidator = advertVariantValidator;
        _advertValidator = advertValidator;
    }


    protected async Task<Result<LineItemDetails>> ValidateAndGetDetailsAsync(
        LineItemCreateRequest request,
        CancellationToken cancellationToken = default)
    {
        Advertisement? advert = null;
        AdvertisementVariant? variant = null;


        if (!request.AdvertisementVariantId.HasValue)
        {
            var advertExists = await _advertValidator
                .GetByIdAsync(request.AdvertisementId, cancellationToken);

            if (!advertExists.IsSuccess || advertExists.Value is null)
            {
                return Result<LineItemDetails>
                    .Fail(advertExists.Error);
            }

            advert = advertExists.Value;
        }
        else
        {
            var variantExists = await _advertVariantValidator.GetByIdIncludeAsync(
                request.AdvertisementVariantId.Value,
                cancellationToken);

            if (!variantExists.IsSuccess || variantExists.Value is null)
            {
                return Result<LineItemDetails>
                    .Fail(variantExists.Error);
            }

            variant = variantExists.Value;

            if (request.AdvertisementId != variant.AdvertisementId)
            {
                return Result<LineItemDetails>
                    .NotFound(EntityNamesResources.Advertisement);
            }
        }

        var itemExists = await _itemValidator.NotExistsAsync(
            request.AdvertisementId,
            request.AdvertisementVariantId,
            request.ParentId,
            cancellationToken
        );

        if (!itemExists.IsSuccess)
        {
            return Result<LineItemDetails>
                .Fail(itemExists.Error);
        }

        var stock = variant?.Stock ?? advert!.Stock;
        var price = variant?.Price ?? advert!.Price;
        var title = variant?.Advertisement.Title ?? advert!.Title;


        if (price is null)
        {
            return Result<LineItemDetails>
                .Fail(string.Format(
                    OrderResources.InvalidPrice,
                    title)
                );
        }


        if (request.Quantity > stock)
        {
            return Result<LineItemDetails>
                .Fail(string.Format(
                    OrderResources.InsufficientStock,
                    title)
                );
        }

        return Result<LineItemDetails>.Success(
            new LineItemDetails(stock, price));
    }
}
