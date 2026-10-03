using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Baskets.BasketItems.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Validator;
using ShagOxServer.Application.Services.Baskets.BasketItems.Mapping;
using ShagOxServer.Application.Services.Baskets.BasketItems.Validator;
using ShagOxServer.Application.Services.Baskets.Core.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets.BasketItems;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Create;
public class BasketItemCreateService
    : IBasketItemCreateService
{
    private readonly IRepository<BasketItem> _itemRepository;
    private readonly BasketItemValidator _itemValidator;

    private readonly BasketValidator _basketValidator;
    private readonly AdvertisementVariantValidator _advertValidator;

    private readonly BasketItemBasketInvalidationService _basketInvalidation;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<BasketItemCreateService> _logger;


    public BasketItemCreateService(
        IRepository<BasketItem> itemRepository,
        BasketItemValidator itemValidator,
        BasketValidator basketValidator,
        AdvertisementVariantValidator advertValidator,
        BasketItemBasketInvalidationService basketInvalidation,
        IUnitOfWork unitOfWork,
        ILogger<BasketItemCreateService> logger)
    {
        _itemRepository = itemRepository;
        _itemValidator = itemValidator;
        _basketValidator = basketValidator;
        _advertValidator = advertValidator;
        _basketInvalidation = basketInvalidation;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        BasketItemCreateRequest request)
    {
        var basketExists = await _basketValidator
            .ExistsByIdAsync(request.BasketId);
        if (!basketExists.IsSuccess)
            return Result<CreateResponse>.Fail(basketExists.Error);

        var variant = await _advertValidator
            .GetByIdAsync(request.AdvertisementVariantId);
        if (!variant.IsSuccess)
            return Result<CreateResponse>.Fail(variant.Error);

        var itemExists = await _itemValidator
           .NotExistsAsync(request.AdvertisementVariantId, request.BasketId);
        if (!itemExists.IsSuccess)
            return Result<CreateResponse>.Fail(itemExists.Error);

        var item = BasketItemCreater.Create(variant.Value!, request);

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

        await _basketInvalidation.InvalidateCreateAsync(
            BasketItemCacheMapper.ToInfo(item));

        return Result<CreateResponse>.Success(
            new CreateResponse(
                item.Id,
                DateTime.UtcNow
        ));
    }
}