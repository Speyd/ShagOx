using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.LineItems.Core.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Validator;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.Baskets.BasketItems.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets.BasketItems;
using ShagOxServer.Application.Services.LineItems.Core.Create;
using ShagOxServer.Application.Services.LineItems.Core.Mapping;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Create;
public class BasketItemCreateService
    : LineItemCreateService<BasketItem>,
    IBasketItemCreateService
{
    private readonly IRepository<BasketItem> _itemRepository;

    private readonly BasketItemBasketInvalidationService _basketInvalidation;

    private readonly BasketItemInvalidationService _itemInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<BasketItemCreateService> _logger;


    public BasketItemCreateService(
        IRepository<BasketItem> itemRepository,
        BasketItemValidator itemValidator,
        AdvertisementVariantValidator advertVariantValidator,
        AdvertisementValidator advertValidator,
        BasketItemBasketInvalidationService basketInvalidation,
        BasketItemInvalidationService itemInvalid,
        IUnitOfWork unitOfWork,
        ILogger<BasketItemCreateService> logger
    )
        : base(itemValidator, 
            advertVariantValidator,
            advertValidator)
    {
        _itemRepository = itemRepository;
        _basketInvalidation = basketInvalidation;
        _itemInvalid = itemInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        LineItemCreateRequest request,
        CancellationToken cancellationToken = default)
    {
        var validation = await ValidateAndGetDetailsAsync(
            request,
            cancellationToken);

        if (!validation.IsSuccess)
            return Result<CreateResponse>.Fail(validation.Error);

        var item = BasketItemCreater
            .Create(request, validation.Value!.Stock);

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
                "BasketId: {ParentId}, AdvertisementVariantId: {AdvertisementVariantId}",
                request.ParentId,
                request.AdvertisementVariantId);

            return Result<CreateResponse>
                .Fail(EntityErrorResources.BasketItemCreateFailed);
        }

        _logger.LogInformation(
            "Basket item created successfully. " +
            "BasketItemId: {BasketItemId}, BasketId: {ParentId}, " +
            "AdvertisementVariantId: {AdvertisementVariantId}",
            item.Id,
            request.ParentId,
            request.AdvertisementVariantId);

        var caheInfo = LineItemCacheMapper.ToInfo(item);

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
}
