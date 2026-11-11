using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Baskets.BasketItems.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Validator;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.Baskets.BasketItems.Mapping;
using ShagOxServer.Application.Services.Baskets.BasketItems.Validator;
using ShagOxServer.Application.Services.Baskets.Core.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets.BasketItems;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.SharedKernel.Abstractions.Results;
using Twilio.Rest.Verify.V2.Service;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Create;
public class BasketItemCreateService
    : IBasketItemCreateService
{
    private readonly IRepository<BasketItem> _itemRepository;
    private readonly BasketItemValidator _itemValidator;

    private readonly BasketValidator _basketValidator;
    private readonly AdvertisementVariantValidator _advertVariantValidator;
    private readonly AdvertisementValidator _advertValidator;

    private readonly BasketItemBasketInvalidationService _basketInvalidation;

    private readonly BasketItemInvalidationService _itemInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<BasketItemCreateService> _logger;


    public BasketItemCreateService(
        IRepository<BasketItem> itemRepository,
        BasketItemValidator itemValidator,
        BasketValidator basketValidator,
        AdvertisementVariantValidator advertVariantValidator,
        AdvertisementValidator advertValidator,
        BasketItemBasketInvalidationService basketInvalidation,
        BasketItemInvalidationService itemInvalid,
        IUnitOfWork unitOfWork,
        ILogger<BasketItemCreateService> logger)
    {
        _itemRepository = itemRepository;
        _itemValidator = itemValidator;
        _basketValidator = basketValidator;
        _advertVariantValidator = advertVariantValidator;
        _advertValidator = advertValidator;
        _basketInvalidation = basketInvalidation;
        _itemInvalid = itemInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        BasketItemCreateRequest request,
        CancellationToken cancellationToken = default)
    {
        var validation = await ValidateAndGetStockAsync(
            request,
            cancellationToken);

        if (!validation.IsSuccess)
            return Result<CreateResponse>.Fail(validation.Error);

        var item = BasketItemCreater.Create(request, validation.Value!);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _itemRepository.Add(item);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create basket item. " +
                "BasketId: {BasketId}, AdvertisementVariantId: {AdvertisementVariantId}",
                request.BasketId,
                request.AdvertisementVariantId);

            return Result<CreateResponse>
                .Fail(EntityErrorResources.BasketItemCreateFailed);
        }

        _logger.LogInformation(
            "Basket item created successfully. " + 
            "BasketItemId: {BasketItemId}, BasketId: {BasketId}, " +
            "AdvertisementVariantId: {AdvertisementVariantId}",
            item.Id,
            request.BasketId,
            request.AdvertisementVariantId);

        var caheInfo = BasketItemCacheMapper.ToInfo(item);

        await _basketInvalidation.InvalidateCreateAsync(
            caheInfo);

        await _itemInvalid.InvalidateCreateAsync(
            caheInfo);

        return Result<CreateResponse>.Success(
            new CreateResponse(
                item.Id,
                DateTime.UtcNow
        ));
    }

    private async Task<Result<int>> ValidateAndGetStockAsync(
        BasketItemCreateRequest request,
        CancellationToken cancellationToken = default)
    {
        var basketExists = await _basketValidator
            .ExistsByIdAsync(request.BasketId, cancellationToken);
        if (!basketExists.IsSuccess)
            return Result<int>.Fail(basketExists.Error);

        Advertisement? advert = null;
        AdvertisementVariant? variant = null;


        if (!request.AdvertisementVariantId.HasValue)
        {
            var advertExists = await _advertValidator
                .GetByIdAsync(request.AdvertisementId, cancellationToken);

            if (!advertExists.IsSuccess || advertExists.Value is null)
                return Result<int>.Fail(advertExists.Error);

            advert = advertExists.Value;
        }
        else
        {
            var variantExists = await _advertVariantValidator.GetByIdAsync(
                request.AdvertisementVariantId.Value,
                cancellationToken);

            if (!variantExists.IsSuccess || variantExists.Value is null)
                return Result<int>.Fail(variantExists.Error);

            variant = variantExists.Value;

            if (request.AdvertisementId != variant.AdvertisementId)
                return Result<int>.NotFound(EntityNamesResources.Advertisement);
        }

        var itemExists = await _itemValidator.NotExistsAsync(
            request.AdvertisementId,
            request.AdvertisementVariantId,
            request.BasketId, 
            cancellationToken
        );

        if (!itemExists.IsSuccess)
            return Result<int>.Fail(itemExists.Error);

        return Result<int>.Success(variant?.Stock ?? advert!.Stock);
    }
}
