using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Baskets.BasketItems.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Create;
using ShagOxServer.Application.Resources.EntityErrorResourcess;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.Baskets.BasketItems.Validator;
using ShagOxServer.Application.Services.Baskets.Core.Validator;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Baskets.BasketItems.Create;
public class BasketItemCreateService
    : IBasketItemCreateService
{
    private readonly IRepository<BasketItem> _itemRepository;
    private readonly BasketItemValidator _itemValidator;

    private readonly BasketValidator _basketValidator;
    private readonly AdvertisementValidator _advertValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<BasketItemCreateService> _logger;


    public BasketItemCreateService(
        IRepository<BasketItem> itemRepository,
        BasketItemValidator itemValidator,
        BasketValidator basketValidator,
        AdvertisementValidator advertValidator,
        IUnitOfWork unitOfWork,
        ILogger<BasketItemCreateService> logger)
    {
        _itemRepository = itemRepository;
        _itemValidator = itemValidator;
        _basketValidator = basketValidator;
        _advertValidator = advertValidator;
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

        var advertExists = await _advertValidator
            .ExistsByIdAsync(request.AdvertisementId);
        if (!advertExists.IsSuccess)
            return Result<CreateResponse>.Fail(advertExists.Error);

        var itemExists = await _itemValidator
           .NotExistsAsync(request.AdvertisementId, request.BasketId);
        if (!itemExists.IsSuccess)
            return Result<CreateResponse>.Fail(itemExists.Error);

        var item = BasketItemCreater.Create(request);

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
                "BasketId: {BasketId}, AdvertisementId: {AdvertisementId}",
                request.BasketId,
                request.AdvertisementId);

            return Result<CreateResponse>
                .Fail(EntityErrorResources.BasketItemCreateFailed);
        }

        _logger.LogInformation(
            "Basket item created successfully. " + 
            "BasketItemId: {BasketItemId}, BasketId: {BasketId}, " + 
            "AdvertisementId: {AdvertisementId}",
            item.Id,
            request.BasketId,
            request.AdvertisementId);

        return Result<CreateResponse>.Success(
            new CreateResponse(
                item.Id,
                DateTime.UtcNow
        ));
    }
}